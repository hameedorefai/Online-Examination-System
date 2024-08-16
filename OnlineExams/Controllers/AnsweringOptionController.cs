using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataAccess;
namespace OnlineExamsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnsweringOptionController : ControllerBase
    {
        [HttpGet("Options", Name = "GetOptionsByQuestionID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<AnsweringOptionDTO>> GetOptionsByQuestionID(int questionID)
        {
            List<AnsweringOptionDTO> Options = clsAnsweringOption.GetOptionsByQuestionID(questionID);
            if(Options.Count == 0)
            {
                return NotFound("No Options Found!");
            }
            return Ok(Options);
        }
    }
}
