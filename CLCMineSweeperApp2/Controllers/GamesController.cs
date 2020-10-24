using System;
using System.Collections.Generic;
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
        public List<string> Get()
        {
            string connectionString = "Server =.; Database = minesweeperApp; Trusted_Connection = True";
            string query = "SELECT * FROM dbo.Game ";
            List<string> results = new List<string>();
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
        public void Post([FromBody]string value)
        {
        }


        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
