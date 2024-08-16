using BusinessLayer;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {

        [HttpGet("ExamInfo", Name = "GetExamInfoByExamID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetExamInfoByExamID(int ExamID)
        {
            try
            {
                var exam = clsExam.GetExamInfoByExamID(ExamID);
                if (exam.ExamID == 0 || exam == null)
                {
                    return NotFound($"No Exam found for ID '{ExamID}'");
                }
                else
                    return Ok(exam);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("{ExamID}", Name = "GetFullExamByExamID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFullExamByExamID(int ExamID)
        {
            try
            {
                var exam = clsExam.GetFullExamByExamID(ExamID);
                if (exam.ExamID == 0 || exam == null)
                {
                    return NotFound($"No Result found for ID '{ExamID}'");
                }
                else
                    return Ok(exam);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("Info", Name = "GetExamsInfoForCourseID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ExamDTO>> GetExamsInfoForCourseID(int CourseID)
        {
            List<ExamDTO> Exams = clsExam.GetExamsInfoForCourseID(CourseID);
            if (Exams == null || Exams.Count == 0)
            {
                return NotFound("No Exams Found!");
            }
            return Ok(Exams);
        }


        [HttpPost("SubmitAndGetResult", Name = "SubmitAndGetResult")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SubmitAndGetResult([FromBody] SubmitExamination SubmittedExamination)
        {
            try
            {
                if (SubmittedExamination == null || SubmittedExamination.QuestionsList == null || SubmittedExamination.QuestionsList.Count == 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Exam data is invalid or incomplete."
                    });
                }

                var result = clsResult.CalculateAndSaveResult(SubmittedExamination);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                    {
                        Success = false,
                        Message = "Failed to calculate the result."
                    });
                }

                // Example of setting placeholders
                result.TimeSpent = "placeholderTimeSpent";
                foreach (var answer in result.studentAnswersDTO)
                {
                    answer.timeSpent = 1;  // Placeholder value
                }

                return Ok(new ApiResponse<GetResultDTO>
                {
                    Success = true,
                    Message = "Result calculated successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // Log exception (assuming a logging service is available)
              //  _logger.LogError(ex, "Error occurred while submitting exam and calculating result.");

                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                {
                    Success = false,
                    Message = "An error occurred while processing your request. Please try again later."
                });
            }
        }

        // Generic API response wrapper
        public class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public object Data { get; set; } = null;
        }

        public class ApiResponse<T> : ApiResponse
        {
            public new T Data { get; set; }
        }



        [HttpPost("Add", Name = "AddNewFullExam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult/*<ExamDTO>*/ AddNewFullExam([FromBody] AddExamDTO exam)
        {
            try
            {
                if (clsExam.AddNewFullExam(exam) != 0)
                    return Ok($"Exam Stored Succefully with ExamID '{exam.ExamID}'.");
                else
                    return BadRequest("Failed to store the exam.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " +ex.Message);
            }
        }




        [HttpPut("Update", Name = "UpdateExamInfoByExamID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateExamInfoByExamID([FromBody] UpdateExamDTO exam)
        {
            try
            {
                if (clsExam.UpdateExamInfoByExamID(exam))
                    return Ok($"Exam with id '{exam.ExamID}' has been updated Succefully.");
                else
                    return BadRequest("Failed to update the exam, or the exam does not exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }

        [HttpDelete("Delete", Name = "DeleteFullExam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteFullExam(int ExamID)
        {
            try
            {
                if (clsExam.DeleteExam(ExamID))
                    return Ok($"Exam with ID '{ExamID}' has been deleted Succefully with its questions and options.");
                else
                    return BadRequest("Failed to delete exam.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }
    }
}
