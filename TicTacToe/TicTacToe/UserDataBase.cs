using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class UserDataBase : IRepository
    { 
        public string[] AddUserToDataBase(User value)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=.;Initial Catalog=TicTacToe Database;User ID=Sa;Password=test123!@#";
            connection.Open();
            SqlCommand cmd = new SqlCommand("select AccessToken from UserInfo where UserName='" + value.UserName + "'", connection);
            string accessToken = "";
            string userCreation = "";
            try
            {
                accessToken = cmd.ExecuteScalar().ToString();
                userCreation = "User Already Existed";
            }
            catch
            {
                accessToken = Guid.NewGuid().ToString();
                SqlCommand command = new SqlCommand("insert into UserInfo values(@Id,@FirstName,@LastName,@UserName,@AccessToken)", connection);
                command.Parameters.AddWithValue("@Id", value.Id);
                command.Parameters.AddWithValue("@FirstName", value.FirstName);
                command.Parameters.AddWithValue("@LastName", value.LastName);
                command.Parameters.AddWithValue("@UserName", value.UserName);
                command.Parameters.AddWithValue("@AccessToken", accessToken);
                userCreation = "New User Created";
                command.ExecuteNonQuery();
            }
            string[] arr = { accessToken, userCreation }; 
            connection.Close();
            return arr;
        }

        public bool checkExistence(string key)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=.;Initial Catalog=TicTacToe Database;User ID=Sa;Password=test123!@#";
            connection.Open();
            SqlCommand cmd = new SqlCommand("select Id from UserInfo where AccessToken='" + key + "'", connection);
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void LogDatabase(Logger logger)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=.;Initial Catalog=TicTacToe Database;User ID=Sa;Password=test123!@#";
            connection.Open();
            SqlCommand command = new SqlCommand("insert into ApplicationLog values(@Request,@Response,@Exception,@Status)", connection);
            command.Parameters.AddWithValue("@Request", logger.Request);
            command.Parameters.AddWithValue("@Response", logger.Response);
            command.Parameters.AddWithValue("@Exception", logger.Exception);
            command.Parameters.AddWithValue("@Status", logger.Status);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
