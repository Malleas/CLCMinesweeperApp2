
using CLCMinesweeperApp.Models;

using CLCMinesweeperApp.Services.Data;
using CLCMineSweeperApp2.Controllers;
using CLCMineSweeperApp2.Utilities.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace CLCMinesweeperApp.Controllers
{
    public class GameBoardController : Controller
    {
        [Dependency]
        public GamesController gameService { get; set; }
        public ILogger logger { get; set; }


        static private int size = 12;
        static private int difficulty = Board.Difficulty;
        static private Board board = new Board(size, difficulty);
        int liveCount = 0;
        int visitedCount = 0;
        int clickCount = 0;
       
        // GET: GameBoard
        public ActionResult Index()
        {

            return View("LoadGame");
        }

      

        [HttpPost]

        public ActionResult DifficultyClick(string difficulty)
        {
            
            if (difficulty.Equals("1"))
            {
                Board.Difficulty = 2;

            }
            else if (difficulty.Equals("2"))
            {
                Board.Difficulty = 66;

            }
            else
            {
                Board.Difficulty = 99;

            }
            SetGameBoard();
            return View("Game", board);
        }

        [HttpPost]

        public ActionResult ContinueGame(string continueBtn)
        {
            if (continueBtn.Equals("1"))
            {
                Environment.Exit(0);
            }

            Board.GameOver = false;
            return View("Difficulty");

        }
        [HttpPost]

        public ActionResult LoadGameClick(string gameTypeBtn)
        {
            if (gameTypeBtn.Equals("0"))
            {
                return View("Difficulty");
            }
            else
            {
                GamesController game = new GamesController();
                 

                List<Cell> gamePieces = new List<Cell>();

                gamePieces = game.LoadGame();

                board.Grid = new Cell[size, size];
                
                foreach (var piece in gamePieces)
                {
                    board.Grid[piece.Row, piece.Column] = piece ;
                }
                
                //some logic here to call loadGame() service to pass the board to GameBoard View.
                return View("Game", board);
            }
        }

        [HttpPost]

        public ActionResult SaveGame(string Value)
        {
            
            string[] strArr = Value.Split('|');
            int time = int.Parse(strArr[0]);
            int clicks = int.Parse(strArr[1]);
            gameService.SaveStats(time, clicks);


            List<Cell> gameCells = new List<Cell>();
            foreach (var cell in board.Grid)
            {
                gameCells.Add(cell);
            }
            
            GameObject gameObject = new GameObject(JsonConvert.SerializeObject(gameCells));
            bool success = gameService.SaveGame(gameObject);
            return View("Results", success);
        }

        [HttpPost]

        public PartialViewResult onRightClick(string button)
        {
            
            string[] strArr = button.Split('|');
            int row = int.Parse(strArr[0]);
            int col = int.Parse(strArr[1]);
            board.Grid[row, col].Flag = true;
            return PartialView("_GameBoard", board);
        }

        [HttpPost]

        public ActionResult OnClick(string button)
        {

            string[] strArr = button.Split('|');
            int row = int.Parse(strArr[0]);
            int col = int.Parse(strArr[1]);
            int currentLiveCount = 0;
            int currentVisitedCount = 0;

            logger.Info("Left clicked on cell: " + row + "," + col);

            clickCount = clickCount + 1;
            Console.Write("Click Count = " + clickCount);

            Console.Write("checking the clicked button value");
            if (board.Grid[row, col].Live)
            {
                foreach (var cell in board.Grid)
                {
                    //Console.Write("clicked a bomb");
                    cell.Visited = true;
                    
                    Board.GameOver = true;

                }
                logger.Info("Left clicked on cell: " + row + "," + col + " Cell was a bomb!");
                return PartialView("_GameBoard", board);
            }
            else
            {
                floodFill(row, col);
            }
            foreach (var cell in board.Grid)
            {
                if (cell.Live)
                {
                    currentLiveCount += 1;
                }
                if (cell.Visited)
                {
                    currentVisitedCount += 1;
                }
            }
            if (currentVisitedCount > visitedCount)
            {
                visitedCount = visitedCount + (currentVisitedCount - liveCount);
            }
            if (currentLiveCount > liveCount)
            {
                liveCount = liveCount + (currentLiveCount - liveCount);
            }
            if (visitedCount == (144 - liveCount))
            {
                foreach (var cell in board.Grid)
                {
                    cell.Visited = true;
                    Board.GameOver = true;
                }
            }

            return PartialView("_Gameboard", board);
        }

        public void SetGameBoard()
        {
            //for each location on the grid, create a new cell and set the row/column to current index location.  Set live and visited to false.  Neighbors can be set to the number of neighboring cells that are not live.
            // if [0,0] has three neighbors and all are not live, neighbors would be set to 3.
            board.Grid = new Cell[size, size];

            for (int i = 0; i < board.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < board.Grid.GetLength(1); j++)
                {
                    Cell cell = new Cell();
                    cell.Row = i;
                    cell.Column = j;
                    cell.Live = false;
                    cell.Visited = false;
                    cell.Neighbors = 0;
                    cell.Flag = false;
                    board.Grid[i, j] = cell;
                }
            }

            SetupLiveNeighbors(Board.Difficulty);

            foreach (var cell in board.Grid)
            {
                calculateLIveNeighbors(cell.Row, cell.Column);
            }
        }

        public void SetupLiveNeighbors(int difficulty)
        {
            Random rand = new Random();

            for (int i = 0; i <= difficulty; i++)
            {
                int randomRow = rand.Next(0, 11);
                int randomColumn = rand.Next(0, 11);

                board.Grid[randomRow, randomColumn].Live = true;

            }
            //initialize the a random grid with "live" bombs.  Pick cells at randome based on the created grid and set cells live() from false to true
            //use difficulty to make a certain % of the cells live based on difficulty input.  Difficulty == #ofCells / difficulty = % of cells that are live (round up)
        }

        public void calculateLIveNeighbors(int row, int column)
        {


            if (board.Grid[row, column].Live)
            {
                board.Grid[row, column].Neighbors += 1;
            }
            board.Grid[row, column].Neighbors += checkTopLeft(row, column);
            board.Grid[row, column].Neighbors += checkTopCenter(row, column);
            board.Grid[row, column].Neighbors += checkTopRight(row, column);
            board.Grid[row, column].Neighbors += checkCenterLeft(row, column);
            board.Grid[row, column].Neighbors += checkCenterRight(row, column);
            board.Grid[row, column].Neighbors += checkBottomLeft(row, column);
            board.Grid[row, column].Neighbors += checkBottomCenter(row, column);
            board.Grid[row, column].Neighbors += checkBottomRight(row, column);

            //need to know current location.
            //  eval current location and the surrounding squares.  if location is 2,2  you can draw a square around it.  Use current and move through each surrounding sqare to see if is is live or not.  if live, += live,
            // return live and use it to update the value for Neighbors for each cell


            //A cell can have 0-8 live neighbors.  if the cell itself is live, the value can be set to 9
        }

        public int checkTopLeft(int row, int col)
        {
            int calcRow = row - 1;
            int calcCol = col - 1;
            int calcLive = 0;
            if (calcRow < 0 || calcCol < 0)
            {
                calcLive = 0;
            }
            else if (board.Grid[calcRow, calcCol].Live)
            {
                calcLive = 1;
            }
            return calcLive;

        }
        public int checkTopCenter(int row, int col)
        {
            int calcRow = row - 1;
            int calcLive = 0;
            if (calcRow < 0)
            {
                calcLive = 0;
            }
            else if (board.Grid[row - 1, col].Live)
            {
                calcLive = 1;
            }
            return calcLive;
        }

        public int checkTopRight(int row, int col)
        {
            int calcRow = row - 1;
            int calcCol = col + 1;
            int calcLive = 0;
            if (calcRow < 0 || calcCol > 11)
            {
                calcLive = 0;
            }
            else if (board.Grid[row - 1, col + 1].Live)
            {
                calcLive = 1;
            }
            return calcLive;
        }

        public int checkCenterLeft(int row, int col)
        {

            int calcCol = col - 1;
            int calcLive = 0;
            if (calcCol < 0)
            {
                calcLive = 0;
            }
            else if (board.Grid[row, col - 1].Live)
            {
                calcLive = 1;
            }
            return calcLive;
        }

        public int checkCenterRight(int row, int col)
        {
            int calcCol = col + 1;
            int calcLive = 0;
            if (calcCol > 11)
            {
                calcLive = 0;
            }
            else if (board.Grid[row, col + 1].Live)
            {
                calcLive = 1;
            }
            return calcLive;
        }
        public int checkBottomLeft(int row, int col)
        {
            int calcRow = row + 1;
            int calcCol = col - 1;
            int calcLive = 0;
            if (calcRow > 11 || calcCol < 0)
            {
                calcLive = 0;
            }
            else if (board.Grid[row + 1, col - 1].Live)
            {
                calcLive = 1;
            }
            return calcLive;
        }
        public int checkBottomCenter(int row, int col)
        {
            int calcRow = row + 1;
            int calcLive = 0;
            if (calcRow > 11)
            {
                calcLive = 0;
            }
            else if (board.Grid[row + 1, col].Live)
            {
                calcLive = 1;
            }
            return calcLive;
        }
        public int checkBottomRight(int row, int col)
        {
            int calcRow = row + 1;
            int calcCol = col + 1;
            int calcLive = 0;
            if (calcRow > 11 || calcCol > 11)
            {
                calcLive = 0;
            }
            else if (board.Grid[row + 1, col + 1].Live)
            {
                calcLive = 1;
            }
            return calcLive;
        }
        static bool isValid(int row, int col)
        {
            if (row >= 0 && row < size && col >= 0 && col < size)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        static public void floodFill(int row, int col)
        {

            if (!isValid(row, col) || board.Grid[row, col].Visited)
            {
                return;
            }
            board.Grid[row, col].Visited = true;
            if (board.Grid[row, col].Neighbors == 0)
            {

                floodFill(row + 1, col);
                floodFill(row - 1, col);
                floodFill(row, col + 1);
                floodFill(row, col - 1);
            }

        }
    }
}