using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace DataAccess
{
    public class StudentAnswerDTO
    {
        public int questionID;
        public int userID; // student
        public int timeSpent;
        public int selectedOptionID;
        public int resultID;
        public int CorrectAnswerID;
        public bool IsCorrect = false;
        public StudentAnswerDTO()
        {
            
        }
    }
    public class clsStudentAnswerData //: IStudentAnswersData
    {
        public static int AddStudentAnswer(StudentAnswerDTO optionDTO)
        {
            int answerId = 0;
            if (optionDTO.timeSpent < 0)
                optionDTO.timeSpent = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddStudentAnswer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@ResultID", optionDTO.resultID);
                        command.Parameters.AddWithValue("@QuestionID", optionDTO.questionID);
                        command.Parameters.AddWithValue("@SelectedOption", optionDTO.selectedOptionID.ToString()); // You need to change it to INT.
                        command.Parameters.AddWithValue("@UserID", optionDTO.userID);
                        command.Parameters.AddWithValue("@TimeSpent", optionDTO.timeSpent);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@AnswerID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        answerId = Convert.ToInt32(outputParam.Value);
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

            return answerId;
        }

        static public StudentAnswerDTO GetStudentAnswerByQuestionID(int questionID)
        {
            StudentAnswerDTO studentAnswerDataDTO = new StudentAnswerDTO();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetStudentAnswerByQuestionID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@QuestionID", questionID);

                        // Adding output parameters
                        SqlParameter answerIdParam = new SqlParameter("@AnswerID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(answerIdParam);

                        SqlParameter resultIdParam = new SqlParameter("@ResultID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(resultIdParam);

                        SqlParameter selectedOptionParam = new SqlParameter("@SelectedOption", SqlDbType.NVarChar, -1) // MAX
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(selectedOptionParam);

                        SqlParameter userIdParam = new SqlParameter("@UserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(userIdParam);

                        SqlParameter timeSpentParam = new SqlParameter("@TimeSpent", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(timeSpentParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter values
                    //    studentAnswerDataDTO.answerID = (int)answerIdParam.Value;
                        studentAnswerDataDTO.resultID = (int)resultIdParam.Value;
                        studentAnswerDataDTO.selectedOptionID = (int)selectedOptionParam.Value;
                        studentAnswerDataDTO.userID = (int)userIdParam.Value;
                        studentAnswerDataDTO.timeSpent = (int)timeSpentParam.Value;

                        return studentAnswerDataDTO;
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

        static public StudentAnswerDTO GetStudentAnswersByResultID(int resultID)
        {
            StudentAnswerDTO studentAnswerDataDTO = new StudentAnswerDTO();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetStudentAnswersByResultID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@ResultID", resultID);

                        // Adding output parameters
                        SqlParameter questionIdParam = new SqlParameter("@QuestionID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(questionIdParam);

                        SqlParameter answerIdParam = new SqlParameter("@AnswerID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(answerIdParam);

                        SqlParameter selectedOptionParam = new SqlParameter("@SelectedOption", SqlDbType.NVarChar, -1) // MAX
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(selectedOptionParam);

                        SqlParameter userIdParam = new SqlParameter("@UserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(userIdParam);

                        SqlParameter timeSpentParam = new SqlParameter("@TimeSpent", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(timeSpentParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter values
                        studentAnswerDataDTO.questionID = (int)questionIdParam.Value;
                     //   studentAnswerDataDTO.answerID = (int)answerIdParam.Value;
                        studentAnswerDataDTO.selectedOptionID = (int)selectedOptionParam.Value;
                        studentAnswerDataDTO.userID = (int)userIdParam.Value;
                        studentAnswerDataDTO.timeSpent = (int)timeSpentParam.Value;

                        return studentAnswerDataDTO;
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
