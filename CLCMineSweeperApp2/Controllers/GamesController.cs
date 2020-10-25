using CLCMinesweeperApp.Models;
using CLCMinesweeperApp.Services.Data;
using CLCMineSweeperApp2.Models;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace CLCMineSweeperApp2.Controllers
{
    public class GamesController : ApiController
    {
        // GET: api/Games
        public List<Cell> LoadGame()
        {
            string connectionString = "Server =.; Database = minesweeperApp; Trusted_Connection = True";
            string query = "Select TOP (1) * from dbo.Game Order by id desc";
            List<Cell> results = new List<Cell>();
            //bool results = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            List<Cell> cells = JsonConvert.DeserializeObject<List<Cell>>(reader.GetString(1));
                            
                            results = cells;
                        }
                    }


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

        // GET: api/Default/5
        public List<string> Get(string id)
        {
            string connectionString = "Server =.; Database = minesweeperApp; Trusted_Connection = True";
            string query = "SELECT * FROM dbo.Game WHERE Id = (@id) ";
            List<string> results = new List<string>();
            //bool results = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            results.Add(reader.GetString(1));
                        }
                    }


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

        // POST: api/Default
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

        public bool SaveStats(int time, int clicks)
        {
            string connectionString = "Server =.; Database = minesweeperApp; Trusted_Connection = True";
            string query = "INSERT INTO dbo.Stats (Time,Clicks) VALUES (@Time,@Clicks) ";
            bool results = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Time",time);
                    command.Parameters.AddWithValue("@Clicks", clicks);
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


        // DELETE: api/Default/5
        public void Delete(int id)
        {
          
        }
    }
}
