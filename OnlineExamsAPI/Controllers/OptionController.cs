using BusinessLayer;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {




        [HttpPut("Update", Name = "UpdateOptionInfoByOptionID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateOptionInfoByOptionID([FromBody] UpdateOptionDTO OptionDTO)
        {
            try
            {
                if (clsAnsweringOption.UpdateOption(OptionDTO))
                    return Ok($"Option with id '{OptionDTO.OptionID}' has been updated Succefully.");
                else
                    return BadRequest($"Failed to update Option, or Option with id '{OptionDTO.OptionID}'does not exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }

        [HttpDelete("Delete", Name = "DeleteOptionByOptionID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteOptionByOptionID(int OptionID)
        {
            try
            {
                if (clsAnsweringOption.DeleteOptionByOptionID(OptionID))
                    return Ok($"Option with ID '{OptionID}' has been deleted Succefully.");
                else
                    return BadRequest("Failed to delete option.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }



    }
}
