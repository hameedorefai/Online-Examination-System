using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccess
{
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string CourseNo { get; set; }
        public string CourseName { get; set; }
        public CourseDTO()
        {

        }
        public CourseDTO(int courseID, string courseNo, string courseName)
        {
            CourseID = courseID;
            CourseNo = courseNo;
            CourseName = courseName;
        }
    }
    public class AddCourseDTO
    {
        public string CourseNo { get; set; }
        public string CourseName { get; set; }
        public AddCourseDTO()
        {

        }
        public AddCourseDTO(string courseNo, string courseName)
        {
            CourseNo = courseNo;
            CourseName = courseName;
        }
    }
    static public class clsCourseData //: ICoursesData
    {
        static public CourseDTO GetCourseInfoByCourseID(int CourseID)
        {
            CourseDTO courseDTO = new CourseDTO();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@CourseID", CourseID);


                        // Execute the stored procedure
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            courseDTO.CourseNo = reader.IsDBNull(reader.GetOrdinal("CourseNo")) ? string.Empty : reader.GetString(reader.GetOrdinal("CourseNo"));
                            courseDTO.CourseName = reader.IsDBNull(reader.GetOrdinal("CourseName")) ? string.Empty : reader.GetString(reader.GetOrdinal("CourseName"));
                            courseDTO.CourseID = CourseID;
                            return courseDTO;
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
            return courseDTO;
        }
        static public  List<CourseDTO> GetCoursesList()
        {
            List<CourseDTO> CoursesDTOList = new List<CourseDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetCoursesList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CourseDTO courseDTO = new CourseDTO();
                                courseDTO.CourseID = reader.GetInt32(reader.GetOrdinal("CourseID"));
                                courseDTO.CourseNo = reader.IsDBNull(reader.GetOrdinal("CourseNo")) ? string.Empty : reader.GetString(reader.GetOrdinal("CourseNo"));
                                courseDTO.CourseName = reader.IsDBNull(reader.GetOrdinal("CourseName")) ? string.Empty : reader.GetString(reader.GetOrdinal("CourseName"));
                                CoursesDTOList.Add(courseDTO);
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
            return CoursesDTOList;
        }
        public static int AddCourse(AddCourseDTO course)
        {
            int courseId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@CourseNo", course.CourseNo);
                        command.Parameters.AddWithValue("@CourseName", course.CourseName);

                        // Adding output parameter
                        SqlParameter outputParam = new SqlParameter("@CourseID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                        courseId = Convert.ToInt32(outputParam.Value);
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

            return courseId;
        }
        public static bool UpdateCourse(CourseDTO course)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameters
                        command.Parameters.AddWithValue("@CourseID", course.CourseID);
                        command.Parameters.AddWithValue("@CourseNo", course.CourseNo);
                        command.Parameters.AddWithValue("@CourseName", course.CourseName);

                        // Execute the stored procedure
                        return (command.ExecuteNonQuery() > 0);
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
        public static bool DeleteCourseByCourseID(int courseID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding input parameter
                        command.Parameters.AddWithValue("@CourseID", courseID);

                        // Execute the stored procedure
                        return (command.ExecuteNonQuery() > 0);
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
