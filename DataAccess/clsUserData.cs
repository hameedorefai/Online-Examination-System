using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
namespace DataAccess
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string RoleName { get; set; }
        public UserDTO()
        {

        }
        public UserDTO(int userID, string username, string email, string roleName)
        {
            UserID = userID;
            this.username = username;
            this.email = email;
            RoleName = roleName;
        }
    }
    public class AddUpdateUserDTO
    {

        public int UserID{ get; set; }
        public string username { get; set; }
        public string passwrod { get; set; }
        public string email { get; set; }

        public AddUpdateUserDTO()
        {

        }
        public AddUpdateUserDTO(string username, string passwrod, string email)
        {
            this.username = username;
            this.passwrod = passwrod;
            this.email = email;
        }
        public AddUpdateUserDTO(string username, string passwrod, string email,int userID)
        {
            this.username = username;
            this.passwrod = passwrod;
            this.email = email;
            this.UserID = userID;
        }
    }
    public class clsUserData
    {

        public static int AddNewUser(AddUpdateUserDTO userDTO)
        {
            int userId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@Username", userDTO.username);
                        command.Parameters.AddWithValue("@Password", userDTO.passwrod);
                        command.Parameters.AddWithValue("@Email", userDTO.email);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@UserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        userId = Convert.ToInt32(outputParam.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

            return userId;
        }

        static public UserDTO GetUserInfoByUserID(int userId)
        {
            UserDTO userDTO = new UserDTO();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetUserInfoByUserID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        // Adding input parameter
                        command.Parameters.AddWithValue("@UserID", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userDTO = new UserDTO()
                                {
                                    username = reader.GetString(reader.GetOrdinal("Username")),
                                    email = reader.GetString(reader.GetOrdinal("Email")),
                                    RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                                    UserID = userId

                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            return userDTO;
        }
        static public List<UserDTO> GetUsersList()
        {
            List<UserDTO> userDTOList = new List<UserDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetUsersList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userDTOList.Add(new UserDTO()
                                {
                                    username = reader.GetString(reader.GetOrdinal("Username")),
                                    email = reader.GetString(reader.GetOrdinal("Email")),
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    RoleName = reader.GetString(reader.GetOrdinal("RoleName"))

                                }
                                );
                            };
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            return userDTOList;
        }
        static public bool UpdateUserInfo(AddUpdateUserDTO userDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateUserInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        command.Parameters.AddWithValue("@UserID", userDTO.UserID);
                        command.Parameters.AddWithValue("@Username", userDTO.username);
                        command.Parameters.AddWithValue("@Email", userDTO.email);

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }
        static public bool UpdateUserPassword(AddUpdateUserDTO userDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateUserPassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        command.Parameters.AddWithValue("@UserID", userDTO.UserID);
                        command.Parameters.AddWithValue("@Password", userDTO.passwrod);

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }

        static public bool DeleteUser(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameter
                        command.Parameters.AddWithValue("@UserID", userId);

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }

        public static (int? UserID, string ErrorMessage) Login(string username, string password)
        {
            int? userID = null;
            string errorMessage = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_LoginUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        // Adding output parameters
                        SqlParameter userIDParam = new SqlParameter("@UserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(userIDParam);

                        SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(errorMessageParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve output parameters
                        userID = userIDParam.Value != DBNull.Value ? (int?)Convert.ToInt32(userIDParam.Value) : null;
                        errorMessage = errorMessageParam.Value.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

            return (userID, errorMessage);
        }

    }
}