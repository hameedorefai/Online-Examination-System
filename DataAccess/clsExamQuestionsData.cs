using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    // DTO Class Definition


    public class clsExamQuestionsData //: IExamQuestionsData
    {
        QuestionDTO questionDTO;
        public static int AddNewExamQuestion(int examID, int questionID)
        {
            int examQuestionId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewExamQuestion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@ExamID", examID);
                        command.Parameters.AddWithValue("@QuestionID", questionID);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@ExamQuestionID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        examQuestionId = Convert.ToInt32(outputParam.Value);
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

            return examQuestionId;
        }

        public static List<QuestionDTO> GetExamQuestionsByExamID(int examID)
        {
            List<QuestionDTO> questions = new List<QuestionDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetExamQuestions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@ExamID", examID);

                        // Execute the stored procedure and read the results
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                questions.Add(  new QuestionDTO
                                {
                                    QuestionID = Convert.ToInt32(reader["QuestionID"]),
                                    QuestionText = reader["QuestionText"].ToString(),
        //                            QuestionTypeName = reader["QuestionTypeName"].ToString()
                                });
                            }
                        }
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

            return questions;
        }

        public static bool DeleteExamQuestionByExamID(int ExamID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteExamQuestionByExamID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameter
                        command.Parameters.AddWithValue("@ExamID", ExamID);

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
    }
}
