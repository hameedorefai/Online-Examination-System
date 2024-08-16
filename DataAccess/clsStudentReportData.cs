using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;



namespace DataAccess
{
    public class ReportDTO
    {
        public int ReportID { get; set; }
        public int UserID { get; set; }
        public string ReportType { get; set; }
        public int? ExamID { get; set; }
        public int? QuestionID { get; set; }
        public int? OptionID { get; set; }
        public string ReportText { get; set; }
        public DateTime ReportDate { get; set; }
        public string Status { get; set; }

        public ReportDTO() { }

        public ReportDTO(int reportID, int userID, string reportType, int? examID, int? questionID, int? optionID, string reportText, DateTime reportDate, string status)
        {
            ReportID = reportID;
            UserID = userID;
            ReportType = reportType;
            ExamID = examID;
            QuestionID = questionID;
            OptionID = optionID;
            ReportText = reportText;
            ReportDate = reportDate;
            Status = status;
        }

        public ReportDTO(int userID, string reportType, int? examID, int? questionID, int? optionID, string reportText)
        {
            UserID = userID;
            ReportType = reportType;
            ExamID = examID;
            QuestionID = questionID;
            OptionID = optionID;
            ReportText = reportText;
            ReportDate = DateTime.Now; // Default to current time
            Status = "Pending"; // Default status
        }
    }
    public class AddReportDTO
    {
        public int UserID { get; set; }
        public string ReportType { get; set; }
        public string ReportText { get; set; }
        public int? ExamID { get; set; }
        public int? QuestionID { get; set; }
        public int? OptionID { get; set; }

        public AddReportDTO() { }

        public AddReportDTO( int userID, string reportType, string reportText, int? examID, int? questionID, int? optionID)
        {
            UserID = userID;
            ReportType = reportType;
            ExamID = examID;
            QuestionID = questionID;
            OptionID = optionID;
            ReportText = reportText;
        }
    }

    public class clsStudentReportData
    {


        static public int AddStudentReport(AddReportDTO report)
        {
            int reportID = 0;

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SP_AddStudentReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@UserID", report.UserID);
                        command.Parameters.AddWithValue("@ReportType", report.ReportType);
                        command.Parameters.AddWithValue("@ExamID", report.ExamID.HasValue ? (object)report.ExamID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@QuestionID", report.QuestionID.HasValue ? (object)report.QuestionID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@OptionID", report.OptionID.HasValue ? (object)report.OptionID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@ReportText", (object)report.ReportText ?? DBNull.Value);

                        // Output parameter
                        var reportIDParam = new SqlParameter("@ReportID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(reportIDParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        reportID = Convert.ToInt32(reportIDParam.Value);
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

            return reportID;
        }

        static public List<ReportDTO> GetAllReports()
        {
            var reports = new List<ReportDTO>();

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SP_GetAllReports", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var report = new ReportDTO
                                {
                                    ReportID = reader.GetInt32(reader.GetOrdinal("ReportID")),
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    ReportType = reader.GetString(reader.GetOrdinal("ReportType")),
                                    ExamID = reader.IsDBNull(reader.GetOrdinal("ExamID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ExamID")),
                                    QuestionID = reader.IsDBNull(reader.GetOrdinal("QuestionID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("QuestionID")),
                                    OptionID = reader.IsDBNull(reader.GetOrdinal("OptionID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("OptionID")),
                                    ReportText = reader.IsDBNull(reader.GetOrdinal("ReportText")) ? null : reader.GetString(reader.GetOrdinal("ReportText")),
                                    ReportDate = reader.GetDateTime(reader.GetOrdinal("ReportDate")),
                                    Status = reader.GetString(reader.GetOrdinal("Status"))
                                };

                                reports.Add(report);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw; // Consider logging the exception
            }
            catch (Exception ex)
            {
                throw; // Consider logging the exception
            }

            return reports;
        }

        static public List<ReportDTO> GetReportsByUserID(int userID)
        {
            var reports = new List<ReportDTO>();

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SP_GetReportsByUserID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userID);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var report = new ReportDTO
                                {
                                    ReportID = reader.GetInt32(reader.GetOrdinal("ReportID")),
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    ReportType = reader.GetString(reader.GetOrdinal("ReportType")),
                                    ExamID = reader.IsDBNull(reader.GetOrdinal("ExamID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ExamID")),
                                    QuestionID = reader.IsDBNull(reader.GetOrdinal("QuestionID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("QuestionID")),
                                    OptionID = reader.IsDBNull(reader.GetOrdinal("OptionID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("OptionID")),
                                    ReportText = reader.IsDBNull(reader.GetOrdinal("ReportText")) ? null : reader.GetString(reader.GetOrdinal("ReportText")),
                                    ReportDate = reader.GetDateTime(reader.GetOrdinal("ReportDate")),
                                    Status = reader.GetString(reader.GetOrdinal("Status"))
                                };

                                reports.Add(report);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw; // Consider logging the exception
            }
            catch (Exception ex)
            {
                throw; // Consider logging the exception
            }

            return reports;
        }

        static public ReportDTO GetReportByReportID(int reportID)
        {
            ReportDTO report = null;

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SP_GetReportByReportID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReportID", reportID);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                report = new ReportDTO
                                {
                                    ReportID = reader.GetInt32(reader.GetOrdinal("ReportID")),
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    ReportType = reader.GetString(reader.GetOrdinal("ReportType")),
                                    ExamID = reader.IsDBNull(reader.GetOrdinal("ExamID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ExamID")),
                                    QuestionID = reader.IsDBNull(reader.GetOrdinal("QuestionID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("QuestionID")),
                                    OptionID = reader.IsDBNull(reader.GetOrdinal("OptionID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("OptionID")),
                                    ReportText = reader.IsDBNull(reader.GetOrdinal("ReportText")) ? null : reader.GetString(reader.GetOrdinal("ReportText")),
                                    ReportDate = reader.GetDateTime(reader.GetOrdinal("ReportDate")),
                                    Status = reader.GetString(reader.GetOrdinal("Status"))
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw; // Consider logging the exception
            }
            catch (Exception ex)
            {
                throw; // Consider logging the exception
            }

            return report;
        }

        public static bool MarkReportAsDone(int reportID)
        {
            using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_MarkReportStatusAsDone", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReportID", reportID);

                    connection.Open();
                    return (command.ExecuteNonQuery() > 0); // rows affectted.
                }
            }
        }

    }
}