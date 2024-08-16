using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{

    public class clsQuestionTypeData //: IQuestionTypesData
    {
        public static int AddNewQuestionType(string questionTypeName)
        {
            int questionTypeId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewQuestionType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@QuestionTypeName", questionTypeName);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@QuestionTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        questionTypeId = Convert.ToInt32(outputParam.Value);
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

            return questionTypeId;
        }

        static public bool GetQuestionTypeByQuestionTypeID(int questionTypeID, ref string questionTypeName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetQuestionType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@QuestionTypeID", questionTypeID);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@QuestionTypeName", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        questionTypeName = outputParam.Value.ToString();

                        // Check if the output parameter has a value
                        return !string.IsNullOrEmpty(questionTypeName);
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
        }

        static public bool DeleteQuestionType(int questionTypeID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteQuestionType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameter
                        command.Parameters.AddWithValue("@QuestionTypeID", questionTypeID);

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

        static public int OldAddNewQuestionType(string questionTypeName)
        {
            int questionTypeId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO QuestionTypes (QuestionTypeName) VALUES (@QuestionTypeName); SELECT SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@QuestionTypeName", questionTypeName);
                    questionTypeId = Convert.ToInt32(command.ExecuteScalar());
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
            return questionTypeId;
        }

        static public bool OldGetQuestionTypeByQuestionTypeID(int questionTypeID, ref string questionTypeName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT QuestionTypeName FROM QuestionTypes WHERE QuestionTypeID = @QuestionTypeID;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@QuestionTypeID", questionTypeID);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        questionTypeName = result.ToString();
                        return true;
                    }
                    return false; // Question type not found
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

        static public bool OldUpdateQuestionType(int questionTypeID, string questionTypeName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    string query = "UPDATE QuestionTypes SET QuestionTypeName = @QuestionTypeName WHERE QuestionTypeID = @QuestionTypeID;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@QuestionTypeName", questionTypeName);
                    command.Parameters.AddWithValue("@QuestionTypeID", questionTypeID);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
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

        static public bool OldDeleteQuestionType(int questionTypeID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM QuestionTypes WHERE QuestionTypeID = @QuestionTypeID;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@QuestionTypeID", questionTypeID);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
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
    }
}

