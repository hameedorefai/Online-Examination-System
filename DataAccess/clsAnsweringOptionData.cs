using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class AnsweringOptionDTO
    {
        public int OptionID { get; set; }
        public int QuestionID { get; set; }
        public string OptionText { get; set; }

        public AnsweringOptionDTO()
        {

        }
        public AnsweringOptionDTO(int optionID, int questionID, string optionText)
        {
            OptionID = optionID;
            QuestionID = questionID;
            OptionText = optionText;
        }
        public AnsweringOptionDTO(int optionID, string optionText)
        {
            OptionID = optionID;
            OptionText = optionText;
        }    
    }
    public class UpdateOptionDTO
    {
        public int OptionID { get; set; }
        public string OptionText { get; set; }
    }
        public class clsAnsweringOptionData //: IAnsweringOptionsData
    {
        public static int AddNewOption(AnsweringOptionDTO AnsweringOptionDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewOption", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@QuestionID", AnsweringOptionDTO.QuestionID);
                        command.Parameters.AddWithValue("@OptionText", AnsweringOptionDTO.OptionText);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@NewOptionID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        AnsweringOptionDTO.OptionID = Convert.ToInt32(outputParam.Value);
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

            return AnsweringOptionDTO.OptionID;
        }

        static public List<AnsweringOptionDTO> GetOptionsForQuestion(int questionID)
        {
            List<AnsweringOptionDTO> Options = new List<AnsweringOptionDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetOptionsForQuestion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@QuestionID", questionID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int optionID = reader.GetInt32(reader.GetOrdinal("OptionID"));
                                string optionText = reader.IsDBNull(reader.GetOrdinal("OptionText")) ? string.Empty : reader.GetString(reader.GetOrdinal("OptionText"));
                                Options.Add(new AnsweringOptionDTO(optionID, questionID, optionText));
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

            return Options;
        }

        static public bool UpdateOption(UpdateOptionDTO OptionDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateOption", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        command.Parameters.AddWithValue("@OptionID", OptionDTO.OptionID);
                        command.Parameters.AddWithValue("@OptionText", OptionDTO.OptionText);

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
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

        static public bool DeleteOption(int optionID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteOption", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameter
                        command.Parameters.AddWithValue("@OptionID", optionID);

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
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

    }
}
