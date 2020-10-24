using CLCMinesweeperApp.Models;
using CLCMinesweeperApp.Services.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CLCMinesweeperApp.Services.Business
{
    public class UserService
    {
        public bool CreateUser(Player player)
        {
            string connectionString = "Server =.; Database = minesweeperApp; Trusted_Connection = True";
            string query = @"insert into dbo.Player(FirstName,LastName,Gender,Age,State,EmailAddress,Username,Password) VALUES (@firstName,@lastName,@gender,@age,@state,@emailAddress,@username,@password)";
            bool results = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@firstName", player.firstName);
                    command.Parameters.AddWithValue("@lastName", player.lastname);
                    command.Parameters.AddWithValue("@gender", player.gender.ToString());
                    command.Parameters.AddWithValue("@age", int.Parse(player.age));
                    command.Parameters.AddWithValue("@state", player.state.ToString());
                    command.Parameters.AddWithValue("@emailAddress", player.emailAddress);
                    command.Parameters.AddWithValue("@username", player.userName);
                    command.Parameters.AddWithValue("@password", Encrypt(player.password));

                    command.Connection.Open();
                    int x = command.ExecuteNonQuery();
                    if (x < 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        results = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return results;
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }




    }
}