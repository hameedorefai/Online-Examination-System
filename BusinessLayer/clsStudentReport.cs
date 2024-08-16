using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class clsStudentReport
    {
        static public int AddStudentReport(AddReportDTO report)
        {
            return clsStudentReportData.AddStudentReport(report);
        }

        static public List<ReportDTO> GetAllReports()
        {
            return clsStudentReportData.GetAllReports();
        }

        static public List<ReportDTO> GetReportsByUserID(int userID)
        {
            return clsStudentReportData.GetReportsByUserID(userID);
        }

        static public ReportDTO GetReportByReportID(int reportID)
        {
            return clsStudentReportData.GetReportByReportID(reportID);
        }


        static public bool MarkReportAsDone(int reportId)
        {
            return clsStudentReportData.MarkReportAsDone(reportId);
        }
    }
}
