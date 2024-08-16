using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class ExamDTO
    {
        public int ExamID { get; set; }
        public int CourseID { get; set; }
        public string ExamType { get; set; }
        public int CreatedByUserID { get; set; }
        public List<QuestionDTO> QuestionsList { get; set; }

        // Default constructor
        public ExamDTO()
        {
            QuestionsList = new List<QuestionDTO>();
        }

        // Constructor with main properties
        public ExamDTO(int examID, int courseID, string examType, int createdByUserID)
        {
            ExamID = examID;
            CourseID = courseID;
            ExamType = examType;
            CreatedByUserID = createdByUserID;
            QuestionsList = new List<QuestionDTO>();
        }

        // Constructor with ExamDTO and QuestionsList
        public ExamDTO(ExamDTO examDTO, List<QuestionDTO> questionsList = null)
        {
            ExamID = examDTO.ExamID;
            CourseID = examDTO.CourseID;
            ExamType = examDTO.ExamType;
            CreatedByUserID = examDTO.CreatedByUserID;
            QuestionsList = questionsList ?? new List<QuestionDTO>();
        }
        public ExamDTO(ExamDTO examDTO)
        {
            ExamID = examDTO.ExamID;
            CourseID = examDTO.CourseID;
            ExamType = examDTO.ExamType;
            CreatedByUserID = examDTO.CreatedByUserID;
        }
    }
    public class SubmitExamination
    {
            public int ExamID { get; set; }
            public List<GetQuestionDTO> QuestionsList { get; set; }
            public DateTime StartTime { get; set; } // إضافة StartTime
            public DateTime CompletionTime { get; set; } // إضافة CompletionTime
    }
    public class AddExamDTO
    {
        public int ExamID { get; set; }
        public int CourseID { get; set; }
        public string ExamType { get; set; }
        public int CreatedByUserID { get; set; }
        public List<AddQuestionDTO> AddQuestionsList { get; set; }

        // Default constructor
        public AddExamDTO()
        {
            AddQuestionsList = new List<AddQuestionDTO>();
        }

        // Constructor with main properties
        public AddExamDTO(int examID, int courseID, string examType, int createdByUserID)
        {
            ExamID = examID;
            CourseID = courseID;
            ExamType = examType;
            CreatedByUserID = createdByUserID;
            AddQuestionsList = new List<AddQuestionDTO>();
        }

        public AddExamDTO(ExamDTO examDTO, List<AddQuestionDTO> AddquestionsList)
        {
            ExamID = examDTO.ExamID;
            CourseID = examDTO.CourseID;
            ExamType = examDTO.ExamType;
            CreatedByUserID = examDTO.CreatedByUserID;
            AddQuestionsList = AddquestionsList;
        }
    }
    public class UpdateExamDTO
    {
        public int ExamID { get; set; }
        public int CourseID { get; set; }
        public string ExamType { get; set; }

        // Default constructor
        public UpdateExamDTO()
        {
        }

        // Constructor with main properties
        public UpdateExamDTO(int examID, int courseID, string examType)
        {
            ExamID = examID;
            CourseID = courseID;
            ExamType = examType;
        }

    }

    public class clsExamData
    {
        public static int AddNewExam(AddExamDTO examDTO)
        {
            //  examDTO.CreatedByUserID = 6; //temporary
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewExam", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@CourseID", examDTO.CourseID);
                        command.Parameters.AddWithValue("@ExamType", examDTO.ExamType);
                        command.Parameters.AddWithValue("@CreatedByUserID", examDTO.CreatedByUserID);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@ExamID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        examDTO.ExamID = Convert.ToInt32(outputParam.Value);
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

            return examDTO.ExamID;
        }

        public static ExamDTO GetExamByExamID2(int examID)
        {
            ExamDTO examDTO = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetExam", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@ExamID", examID);

                        // Adding output parameters
                        SqlParameter courseIDParam = new SqlParameter("@CourseID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(courseIDParam);

                        SqlParameter examTypeParam = new SqlParameter("@ExamType", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(examTypeParam);

                        SqlParameter createdByUserIDParam = new SqlParameter("@CreatedByUserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(createdByUserIDParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter values
                        int courseID = Convert.ToInt32(courseIDParam.Value);
                        string examType = examTypeParam.Value.ToString();
                        int createdByUserID = Convert.ToInt32(createdByUserIDParam.Value);

                        if (courseID != 0) // Assuming CourseID will be non-zero if found
                        {
                            examDTO = new ExamDTO(examID, courseID, examType, createdByUserID);
                        }

                        return examDTO;
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

        public static bool UpdateExamInfoByExamID(UpdateExamDTO examDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateExamInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        command.Parameters.AddWithValue("@ExamID", examDTO.ExamID);
                        command.Parameters.AddWithValue("@CourseID", examDTO.CourseID);
                        command.Parameters.AddWithValue("@ExamType", examDTO.ExamType);

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
        public static bool DeleteExam(int examID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteExam", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameter
                        command.Parameters.AddWithValue("@ExamID", examID);

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
        public static List<ExamDTO> GetExamsInfoForCourseID(int courseID)
        {
            List<ExamDTO> examDTOList = new List<ExamDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetExamsInfoForCourseID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@CourseID", courseID);

                        // Execute the stored procedure
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int examID = reader.GetInt32(reader.GetOrdinal("ExamID"));
                                string examType = reader.GetString(reader.GetOrdinal("ExamType"));
                                int createdByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));

                                examDTOList.Add(new ExamDTO(examID, courseID, examType, createdByUserID));
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

            return examDTOList;
        }
        public static ExamDTO GetExamByExamID(int examID)
        {
            ExamDTO examDTO = new ExamDTO();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetExamByExamID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@ExamID", examID);

                        // Execute the stored procedure
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                examDTO = new ExamDTO()
                                {
                                    ExamID = examID,
                                    CourseID = reader.GetInt32(reader.GetOrdinal("CourseID")),
                                    ExamType = reader.GetString(reader.GetOrdinal("ExamType")),
                                    CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))

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
            return examDTO;
        }
    }
}
