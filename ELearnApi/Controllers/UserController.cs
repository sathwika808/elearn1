using ELearnApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ELearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {
        IConfiguration config;
        ELearnDbContext db = null;
        public UserController(IConfiguration config, ELearnDbContext c)
        {
            this.config = config;
    
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

        [HttpPost("Login")]
        public IActionResult Login(UsersDto PostedUsers) {

            if (PostedUsers == null)
            {
                return BadRequest("Logic data is null");
            }
            // logic for login process 
            //If login username and password are correct then proceed to generate token
            var user = db.Users.Where(u => u.Password == PostedUsers.Password && u.Username == PostedUsers.Username).FirstOrDefault();
            if (user == null)
            {


                return BadRequest("Invalid Credentials");
            }
            //generate the JWT token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiry = Convert.ToInt32(config["Jwt:ExpireMinutes"]);
            var Sectoken = new JwtSecurityToken(config["Jwt:Issuer"],
              config["Jwt:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(expiry),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(new { Token = token, UserName = user.Username, UserId = user.id , user.Email , user.Password});

        }

    }




    }

