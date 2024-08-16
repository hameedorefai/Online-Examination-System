using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
namespace DataAccess
{
    public class ResultDTO
    {
        public int ResultID { get; set; }
        public int ExamID { get; set; }
        public int UserID { get; set; }
        public float ScorePersantage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CompletionTime { get; set; }
        public List<StudentAnswerDTO> studentAnswersDTO { get; set; }
        public ResultDTO()
        {
            
        }
    }
    public class GetResultDTO
    {
        public string ScorePersantage { get; set; }
        public string TimeSpent { get; set; }
        public List<StudentAnswerDTO> studentAnswersDTO { get; set; }

        public GetResultDTO(string ScorePersantage, string TimeSpent, List<StudentAnswerDTO> studentAnswers)
        {
            this.ScorePersantage = ScorePersantage;
            this.TimeSpent = TimeSpent;
            this.studentAnswersDTO = studentAnswers;
        }
    }
        public class clsResultData //: IResultsData
    {
        public static int AddNewResult(ResultDTO result)
        {
            int resultId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewResult", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@ExamID", result.ExamID);
                        command.Parameters.AddWithValue("@UserID", result.UserID);
                        command.Parameters.AddWithValue("@Score", result.ScorePersantage);
                        command.Parameters.AddWithValue("@StartTime", result.StartTime);
                        command.Parameters.AddWithValue("@CompletionTime", result.CompletionTime);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@ResultID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        resultId = Convert.ToInt32(outputParam.Value);
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

            return resultId;
        }

        public static ResultDTO GetResultByResultID(int resultID)
        {
            ResultDTO result = new ResultDTO();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetResultByResultID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@ResultID", resultID);

                        // Execute the stored procedure and retrieve the result
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result.ExamID = reader.GetInt32(0); 
                                result.UserID = reader.GetInt32(1); 
                                result.ScorePersantage = reader.GetInt32(2); 
                                result.CompletionTime = reader.GetDateTime(3); 
                                result.ResultID = resultID;
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

            return result;
        }


        public static List<ResultDTO> GetResultsForUserID(int userID)
        {
            List<ResultDTO> resultsList = new List<ResultDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetResultsForUserID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@UserID", userID);

                        // Execute the stored procedure and get the result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ResultDTO resultDTO = new ResultDTO
                                {
                                    ExamID = reader.GetInt32(reader.GetOrdinal("ExamID")),
                                    ResultID = reader.GetInt32(reader.GetOrdinal("ResultID")),
                                    ScorePersantage = reader.GetInt32(reader.GetOrdinal("Score")),
                                    CompletionTime = reader.GetDateTime(reader.GetOrdinal("CompletionTime"))
                                };

                                resultsList.Add(resultDTO);
                            }
                        }
                    }
                }

                return resultsList;
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

        public static List<ResultDTO> GetResultsByUserIDAndExamID(int userID, int examID)
        {
            List<ResultDTO> resultsList = new List<ResultDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetResultsByUserIDAndExamID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@ExamID", examID);

                        // Execute the stored procedure and get the result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ResultDTO resultDTO = new ResultDTO
                                {
                                    ResultID = reader.GetInt32(reader.GetOrdinal("ResultID")),
                                    ScorePersantage = reader.GetInt32(reader.GetOrdinal("Score")),
                                    CompletionTime = reader.GetDateTime(reader.GetOrdinal("CompletionTime"))
                                };

                                resultsList.Add(resultDTO);
                            }
                        }
                    }
                }

                return resultsList;
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

        public static List<ResultDTO> GetResultsByExamID(int examID)
        {
            List<ResultDTO> resultsList = new List<ResultDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetResultsByExamID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@ExamID", examID);

                        // Execute the stored procedure and get the result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ResultDTO resultDTO = new ResultDTO
                                {
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    ResultID = reader.GetInt32(reader.GetOrdinal("ResultID")),
                                    ScorePersantage = reader.GetInt32(reader.GetOrdinal("Score")),
                                    CompletionTime = reader.GetDateTime(reader.GetOrdinal("CompletionTime"))
                                };

                                resultsList.Add(resultDTO);
                            }
                        }
                    }
                }

                return resultsList;
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

        static public bool DeactivateResultByResultID(int resultID)
        { 
            // We don't delete results. instead, I deactivate them.
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeactivateResult", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameter
                        command.Parameters.AddWithValue("@ResultID", resultID);

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
