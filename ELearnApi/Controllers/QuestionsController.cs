using ELearnApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {

        ELearnDbContext db = null;
        public QuestionsController(ELearnDbContext con)
        {
            db = con;   
        }


        [HttpGet]

        public IActionResult GetQuestions()
        {
            var questions = db.Questions.ToList();
            if (questions == null)
            {
                return NotFound("No Feedbacks");
            }
            return Ok(questions);
        }



        [HttpGet("{id}")]

        public IActionResult GetQuestionsById(long id)
        {
            var questions = db.Questions.Find(id);
            if ( questions == null)
            {
                return NotFound($"Feedback with {id} is not found");
            }
            return Ok(questions);
        }



        [HttpPost]
        public IActionResult PostBookMark(Questions postedQuestions)
        {
            if (postedQuestions == null)
            {
                return BadRequest("post valid  bookmark");
            }
            db.Questions.Add(postedQuestions);
            db.SaveChanges();
            return Ok("Posted succesfully");

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteQuestion(int id)
        {
            var QuestionsToBeDeleted = db.Questions.Find(id);
            if (QuestionsToBeDeleted == null)
            {
                return NotFound($"Feedback with id {id} is not found");
            }
            db.Questions.Remove(QuestionsToBeDeleted);
            db.SaveChanges();
            return Ok("deleted Successfully");
        }

    }
}
