using ELearnApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        ELearnDbContext db = null;
        public UserController(ELearnDbContext c)
        {
          db = c;
        }

        [HttpGet]

        public IActionResult GetUser()
        {
            var questions = db.Users.ToList();
            if (questions == null)
            {
                return NotFound("No Feedbacks");
            }
            return Ok(questions);
        }


        [HttpPatch("{id}")]
        public IActionResult EditUser(int id, Users updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest("Invalid user data.");
            }

            var existingUser = db.Users.Find(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;

            

            db.Users.Update(existingUser);
            db.SaveChanges();

            return Ok(existingUser);
        }

      

        [HttpPost]
        public IActionResult postUser(Users PostedUsers)
        {
            if (PostedUsers == null)
            {
                return BadRequest("post valid  bookmark");
            }
            db.Users.Add(PostedUsers);
            db.SaveChanges();
            return Ok("Posted succesfully");

        }
    }
}
