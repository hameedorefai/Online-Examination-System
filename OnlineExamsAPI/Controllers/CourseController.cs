using BusinessLayer;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {


        [HttpGet("Info", Name = "GetCourseInfoByCourseID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCourseInfoByCourseID(int CourseID)
        {
            try
            {
                var CourseDTO = clsCourse.GetCourseInfoByCourseID(CourseID);
                if (CourseDTO.CourseID == 0 || CourseDTO == null)
                {
                    return NotFound($"No Course found for ID '{CourseID}'");
                }
                else
                    return Ok(CourseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("List", Name = "GetCoursesList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CourseDTO>> GetCoursesList()
        {
            try
            {
                var CoursesDTOList = clsCourse.GetCoursesList();
                if (CoursesDTOList == null || CoursesDTOList.Count == 0)
                {
                    return NotFound("No Courses Found!");
                }
                return Ok(CoursesDTOList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddNewCoures([FromBody] AddCourseDTO Course)
        {
            try
            {
                var CourseID = clsCourse.AddCourse(Course);
                if(CourseID > 0)
                return Ok($"Course created succesfully with id '{CourseID}'");
                if (CourseID == -1)
                    return BadRequest("You don't have the permission to do this.");
                return BadRequest("error with storing course data.");


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCourse([FromBody] CourseDTO Course)
        {
            try
            {
                int result = clsCourse.UpdateCourse(Course);
                if (result == 1)
                    return Ok($"Course with id '{Course.CourseID}' has been updated successfully.");
                else if(result== - 1)
                    return BadRequest("You don't have the permission to do this.");
                else
                {
                    return NotFound($"course with id '{Course.CourseID}' was not found or an error occurred.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }



        [HttpDelete("Delete", Name = "DeleteCourse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteCourse(int CourseID)
        {
            try
            { int result = clsCourse.DeleteCourse(CourseID);
                if (result == 1)
                    return Ok($"Course with ID '{CourseID}' has been deleted Succefully.");
                else if (result == -1)
                    return BadRequest("You don't have the permission to do this.");

                else
                    return BadRequest("Failed to delete Course, it could be data related to this course.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }


    }
}

