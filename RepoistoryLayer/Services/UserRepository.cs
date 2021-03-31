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
                    SqlCommand sqlCommand = new SqlCommand("SpAddUserDetails", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                   // sqlCommand.Parameters.AddWithValue("@UserId", userRegistration.UserId);
                    sqlCommand.Parameters.AddWithValue("@FirstName", userRegistration.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", userRegistration.LastName);
                    sqlCommand.Parameters.AddWithValue("@Email", userRegistration.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", userRegistration.Password);
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
    }
}
