using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CLCMinesweeperApp.Services.Data
{
    public class GameDAO
    {
        public bool SaveGame(GameObject gameObject)
        {
            string connectionString = "Server =.; Database = minesweeperApp; Trusted_Connection = True";
            string query = "INSERT INTO dbo.Game (Gameboard) VALUES (@Gameboard) ";
            bool results = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@Gameboard", SqlDbType.Text).Value = gameObject.JsonString;
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    results = true;
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return results;

        }

        public bool LoadGame()
        {
            return true;
        }
    }
}