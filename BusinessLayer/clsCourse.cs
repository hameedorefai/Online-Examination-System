using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer
{
    public class clsCourse
    {
        static public CourseDTO courseDTO = new CourseDTO();
        static public CourseDTO GetCourseInfoByCourseID(int courseID)
        {
            return clsCourseData.GetCourseInfoByCourseID(courseID);

        }
        static public List<CourseDTO> GetCoursesList()
        {
            return clsCourseData.GetCoursesList();
        }


        public static int AddCourse(AddCourseDTO course)
        {
            // for permission purpose.
            clsUser.CheckUserRegisterating();
            if (clsGlobal.CurrentUser.RoleName.ToLower() == "standard" )
                return -1;
                return clsCourseData.AddCourse(course);
        }
        public static int UpdateCourse(CourseDTO course)
        {
            if (clsGlobal.CurrentUser == null||clsGlobal.CurrentUser.RoleName.ToLower() != "admin")
                return -1;

            CourseDTO CurrentCourseData = GetCourseInfoByCourseID(course.CourseID);

            course.CourseNo = course.CourseNo.Trim();
            course.CourseName = course.CourseName.Trim();

            if (course.CourseNo == "string" || course.CourseNo == "" || course.CourseNo == null)
                course.CourseNo = CurrentCourseData.CourseNo;
            if (course.CourseName == "string" || course.CourseName == "" || course.CourseName == null)
                course.CourseName = CurrentCourseData.CourseName;

             if(clsCourseData.UpdateCourse(course))
                return 1;

            return 0;
        }
        public static int DeleteCourse(int courseID)
        {
            if (clsGlobal.CurrentUser == null || clsGlobal.CurrentUser.RoleName.ToLower() != "admin")
                return -1;
            if (clsCourseData.DeleteCourseByCourseID(courseID))
                return 1;
            return 0;
        }


    }
}
