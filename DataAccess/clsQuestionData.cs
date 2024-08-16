using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess
{
    public class GetQuestionDTO
    {
        public int QuestionID { get; set; }
        public int StudentAnswerID { get; set; }
    }
        public class QuestionDTO
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int StudentAnswerID { get; set; }

        public List<AnsweringOptionDTO> optionsDTO { get; set; }
        public QuestionDTO()
        {
        }
        public QuestionDTO(int questionID, string questionText)
        {
            this.QuestionID = questionID;
            this.QuestionText = questionText;
        }
        public QuestionDTO(int questionID, string questionText, List<AnsweringOptionDTO> optionsDTO)
        {
            QuestionID = questionID;
            QuestionText = questionText;
            this.optionsDTO = optionsDTO;
        }
    }
    public class AddQuestionDTO
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeID { get; set; }
        public int CorrectOptionID { get; set; }
        [JsonIgnore]
        public int CreatedByUserID { get; set; }

        public List<AnsweringOptionDTO> optionsDTO { get; set; }
        public AddQuestionDTO()
        {
        }

    }
    public class UpdateQuestionDTO
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeID { get; set; }
        public int CorrectOptionID { get; set; }

        public UpdateQuestionDTO()
        {
        }
    }


    public class clsQuestionData
    {
        public static int AddNewQuestion(AddQuestionDTO questionDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewQuestion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@QuestionText", questionDTO.QuestionText);
                        command.Parameters.AddWithValue("@QuestionTypeID", questionDTO.QuestionTypeID);
                        command.Parameters.AddWithValue("@CorrectOptionID", questionDTO.CorrectOptionID);  //Done.// You must edit this in the database to int not nvarchar.
                        command.Parameters.AddWithValue("@CreatedByUserID", questionDTO.CreatedByUserID);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@QuestionID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        questionDTO.QuestionID = Convert.ToInt32(outputParam.Value);
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

            return questionDTO.QuestionID;
        }

        public static bool UpdateQuestionInfo(UpdateQuestionDTO questionDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateQuestion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        command.Parameters.AddWithValue("@QuestionID", questionDTO.QuestionID);
                        command.Parameters.AddWithValue("@QuestionText", questionDTO.QuestionText);
                        command.Parameters.AddWithValue("@QuestionTypeID", questionDTO.QuestionTypeID);
                        command.Parameters.AddWithValue("@CorrectOptionID", questionDTO.CorrectOptionID);

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

        public static bool DeleteQuestion(int questionID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteQuestion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameter
                        command.Parameters.AddWithValue("@QuestionID", questionID);

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

        public static int GetCorrectAnswerByQuestionID(int questionID)
        {
            int @CorrectOptionID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetCorrectAnswer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@QuestionID", questionID);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@CorrectOptionID", SqlDbType.Int, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        @CorrectOptionID = (int)outputParam.Value;
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

            return @CorrectOptionID;
        }

    }
}
