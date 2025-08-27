using ELearnApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ELearnDbContext db;

        public CourseController(ELearnDbContext context)
        {
            db = context;
        }

        // GET: api/Course
        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = db.Courses
                .Include(c => c.Cards)
                .Select(c => new CourseDTO
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    Cards = c.Cards.Select(card => new CardDTO
                    {
                        Id = card.Id,
                        Question = card.Question,
                        Answer = card.Answer,
                        Favorite = card.Favorite,
                        IsReviewed = card.IsReviewed,
                        CourseId = card.CourseId
                    }).ToList()
                })
                .ToList();

            return Ok(courses);
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            var course = db.Courses
                .Include(c => c.Cards)   // load cards
                .FirstOrDefault(c => c.CourseId == id);

            if (course == null)
                return NotFound($"Course with id {id} not found");

            var courseDto = new CourseDTO
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Description = course.Description,
                Cards = course.Cards.Select(card => new CardDTO
                {
                    Id = card.Id,   
                    Question = card.Question,
                    Answer = card.Answer,
                    Favorite = card.Favorite,
                    IsReviewed = card.IsReviewed,
                    CourseId = card.CourseId
                }).ToList()

            };

            return Ok(courseDto);
        }



        // POST: api/Course
        [HttpPost]
        public IActionResult PostCourse(CourseDTO dto)
        {
            if (dto == null) return BadRequest("Invalid course data");

            var course = new Courses
            {
                Title = dto.Title,
                Description = dto.Description,
                Cards = dto.Cards.Select(card => new Card
                {  Id = card.Id,
                    Question = card.Question,
                    Answer = card.Answer,
                    Favorite = card.Favorite,
                    IsReviewed = card.IsReviewed,
                }).ToList()
            };

            db.Courses.Add(course);
            db.SaveChanges();

            return Ok("Course created successfully with questions");
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = db.Courses.Include(c => c.Cards).FirstOrDefault(c => c.CourseId == id);
            if (course == null) return NotFound($"Course with id {id} not found");

            db.Courses.Remove(course);
            db.SaveChanges();

            return Ok("Course deleted successfully");
        }
    }
}
