using BusinessLayer;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {


        [HttpPut("Update", Name = "UpdateQuestionInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateQuestionInfo([FromBody] UpdateQuestionDTO questionDTO)
        {
            try
            {
                if (clsQuestion.UpdateQuestion(questionDTO))
                    return Ok($"Question with id '{questionDTO.QuestionID}' has been updated Succefully.");
                else
                    return BadRequest("Failed to update question, or question does not exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }
        [HttpDelete("Delete", Name = "DeleteQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteQuestion(int QuestionID)
        {
            try
            {
                if (clsQuestion.DeleteQuestion(QuestionID))
                    return Ok($"Question with ID '{QuestionID}' has been deleted Succefully with its options.");
                else
                    return BadRequest("Failed to delete question.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }



    }
}
