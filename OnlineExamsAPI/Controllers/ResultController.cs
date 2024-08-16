using BusinessLayer;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {



        // here you need to add POST method to insert Student Answers
        // here you need to add Get method to Calculate and retrieve Result to the Student
        // edit: all above were done in ExamController.


        [HttpGet("Result", Name = "GetResultsByResultID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetResultByResultID(int ResultID)
        {
            var result = clsResult.GetResultByResultID(ResultID);
            if (result.ResultID == 0)
            {
                return NotFound($"No Result found for ID '{ResultID}'");
            }
            return Ok(result);
        }

        [HttpGet("ExamResults", Name = "GetResultsByExamID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ResultDTO>> GetResultsByExamID(int ExamID)
        {
            List<ResultDTO> results = clsResult.GetResultsByExamID(ExamID);
            if (results == null || results.Count == 0)
            {
                return NotFound($"No Exams Found for ExamID: {ExamID}");
            }
            return Ok(results);
        }

        [HttpGet("UserResults", Name = "GetResultsForUserID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ResultDTO>> GetResultsForUserID(int UserID)
        {
            List<ResultDTO> results = clsResult.GetResultsForUserID(UserID);
            if (results == null || results.Count == 0)
            {
                return NotFound($"No Exams Found for User with ID: {UserID}");
            }
            return Ok(results);
        }

        [HttpGet("UserExamResults", Name = "GetResultsByUserIDAndExamID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ResultDTO>> GetResultsByUserIDAndExamID(int UserID, int ExamID)
        {
            List<ResultDTO> results = clsResult.GetResultsByUserIDAndExamID(UserID,ExamID);
            if (results == null || results.Count == 0)
            {
                return NotFound("No Exams Found!");
            }
            return Ok(results);
        }



        [HttpDelete("Delete", Name = "DeleteResultByResultID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteResultByResultID(int ResultID)
        {
            try
            {
                if (clsResult.DeleteResultByResultID(ResultID))
                    return Ok($"Result with ID '{ResultID}' has been deleted Succefully.");
                else
                    return BadRequest("Failed to delete result.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }


    }
}
