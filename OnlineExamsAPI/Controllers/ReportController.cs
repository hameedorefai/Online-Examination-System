using BusinessLayer;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace OnlineExamsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddStudentReport([FromBody] AddReportDTO report)
        {
            if (report == null)
            {
                return BadRequest("Report data is null.");
            }

            try
            {
                var reportID = clsStudentReport.AddStudentReport(report);
                return CreatedAtAction(nameof(GetReportByReportID), new { reportID = reportID }, reportID);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }

        [HttpPut]
        [Route("Done")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MarkReportAsDone(int reportID)
        {
            try
            {
                if (clsStudentReport.MarkReportAsDone(reportID))
                    return Ok($"Report with id '{reportID}' has been marked as DONE successfully.");
                else
                {
                    return NotFound($"Report with id '{reportID}' was not found or an error occurred.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReportDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllReports()
        {
            try
            {
                var reports = clsStudentReport.GetAllReports();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }

        [HttpGet]
        [Route("user/{userID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReportDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReportsByUserID(int userID)
        {
            try
            {
                List<ReportDTO> reports = clsStudentReport.GetReportsByUserID(userID);
                if (reports == null || reports.Count == 0)
                {
                    return NotFound($"No reports found for UserID {userID}.");
                }

                return Ok(reports);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }

        [HttpGet]
        [Route("{reportID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReportByReportID(int reportID)
        {
            try
            {
                var report = clsStudentReport.GetReportByReportID(reportID);
                if (report == null)
                {
                    return NotFound($"Report with ID {reportID} not found.");
                }

                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }
    }

}
