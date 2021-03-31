using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepoistoryLayer.IRepoistory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepoistoryLayer.Services
{
    public class UserRepository : IUser
    {
        private readonly IConfiguration Configuration;

        public UserRepository(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public UserDetails Registration(UserRegistration userRegistration)
        {
            UserDetails details = new UserDetails();
            try
            {
                string connectoin = Configuration.GetConnectionString("MyConnection");
                DateTime createdDate;
                createdDate = DateTime.Now;
                using (SqlConnection sqlConnection = new SqlConnection(connectoin))
                {
                    string Password = EncryptedPassword.EncodePasswordToBase64(userRegistration.Password);
                    SqlCommand sqlCommand = new SqlCommand("SpAddUserDetails", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FirstName", userRegistration.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", userRegistration.LastName);
                    sqlCommand.Parameters.AddWithValue("@Email", userRegistration.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    sqlCommand.Parameters.AddWithValue("@Address", userRegistration.Address);
                    sqlCommand.Parameters.AddWithValue("@City", userRegistration.City);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", userRegistration.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@CreatedDate", createdDate);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    Console.WriteLine("output=", reader);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            details.UserId = Convert.ToInt32(reader["UserId"].ToString());
                            details.FirstName = reader["FirstName"].ToString();
                            details.LastName = reader["LastName"].ToString();
                            details.UserRole = reader["UserRole"].ToString();
                            details.Email = reader["Email"].ToString();
                            details.Address = reader["Address"].ToString();
                            details.City = reader["City"].ToString();
                            details.PhoneNumber = reader["PhoneNumber"].ToString();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlConnection.Close();
                }
                return details;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public UserDetails Login(UserLogin user)
        {
            UserDetails details = new UserDetails();
            try
            {
                string connect = Configuration.GetConnectionString("MyConnection");
                //Password encrypted
                string Password = EncryptedPassword.EncodePasswordToBase64(user.Password);
                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("SpAddUserLogin", Connection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    Connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        details.UserId = Convert.ToInt32(reader["UserId"].ToString());
                        details.FirstName = reader["FirstName"].ToString();
                        details.LastName = reader["LastName"].ToString();
                        details.UserRole = reader["UserRole"].ToString();
                        details.Email = reader["Email"].ToString();
                        details.Address = reader["Address"].ToString();
                        details.City = reader["City"].ToString();
                        details.PhoneNumber = reader["PhoneNumber"].ToString();
                    }
                    Connection.Close();
                }
                return details;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
