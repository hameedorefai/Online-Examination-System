using BusinessLayer;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        [HttpGet("Info", Name = "GetUserInfoByUserID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserInfoByUserID(int userID)
        {
            try
            {
                var User = clsUser.GetUserInfoByUserID(userID);
                if (User.UserID == 0 || User == null)
                {
                    return NotFound($"No User found with ID '{userID}'");
                }
                else
                    return Ok(User);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("user/Info", Name = "GetCurrentUserInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCurrentUserInfo()
        {
            try
            {
                var User = clsUser.GetCurrentUserInfo();
                if (User.UserID == 0 || User == null)
                {
                    return NotFound($"No logged in User found");
                }
                else
                    return Ok(User);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }





        [HttpGet("UsersList", Name = "GetUsersList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<UserDTO>> GetUsersList()
        {
            try
            {
                var UsersList = clsUser.GetUsersList();
                if (UsersList == null || UsersList.Count == 0)
                {
                    return NotFound("No Exams Found!");
                }
                return Ok(UsersList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }

        }




        [HttpPost("Register", Name = "RegisterNewUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult RegisterNewUser([FromBody] AddUpdateUserDTO User)
        {
            try
            {
                User.UserID = clsUser.RegisterNewUser(User);
                if (User.UserID != 0)
                    return Ok($"User has been Registered Succefully with id '{User.UserID}'.");
                else
                    return BadRequest("Failed to Register User.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }


        [HttpPut("Update", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateUser([FromBody] AddUpdateUserDTO User)
        {
            try
            {
                if (clsUser.UpdateUser(User))
                    return Ok($"User updated Succefully with UserID '{User.UserID}'.");
                else
                    return BadRequest("Failed to update User.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }




        [HttpDelete("Delete", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteUser(int UserID)
        {
            try
            {
                if (clsUser.DeleteUserByUserID(UserID))
                    return Ok($"User with ID '{UserID}' has been deleted Succefully.");
                else
                    return BadRequest("Failed to delete user.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "The Error : " + ex.Message);
            }
        }


        [HttpPost("login")]
        
        public IActionResult Login([FromForm] string Username, [FromForm] string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return BadRequest("Username and password are required.");
            }

            var (userID, errorMessage) = clsUser.Login(Username, Password);

            if (userID.HasValue)
            {
                // Return user ID or any other success response
                return Ok(new { UserID = userID });
            }
            else
            {
                // Return error message if login fails
                return Unauthorized(new { Message = errorMessage });
            }
        }
  
        [HttpPost("logout")]
        public IActionResult logout()
        {
            int Done = clsUser.Logout();

            if (Done == 1)
            {
                // Return user ID or any other success response
                return Ok("You have logged out succefully.");
            }
            else
            {
                return BadRequest("There is no session running at the moment.");
            }
        }


    }
}
