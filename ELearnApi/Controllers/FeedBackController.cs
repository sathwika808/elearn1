using ELearnApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        ELearnDbContext db = null;
        public FeedBackController(ELearnDbContext para)
        {
            db = para;
        }

        [HttpGet]

        public IActionResult GetFeedback()
        {
            var feedbacks = db.feedback.ToList();
            if (feedbacks == null)
            {
                return NotFound("No Feedbacks");
            }
            return Ok(feedbacks);
        }



        [HttpGet("{id}")]

        public IActionResult GetFeedbackById(int id)
        {
            var feedbacks = db.feedback.Find(id);
            if (feedbacks == null)
            {
                return NotFound($"Feedback with {id} is not found");
            }
            return Ok(feedbacks);
        }



        [HttpPost]

        public IActionResult PostFeedback(Feedback Postedfeedback)
        {
            if (Postedfeedback == null)
            {
                return BadRequest("Post a valid feedback");
            }
            db.feedback.Add(Postedfeedback);
            db.SaveChanges();
            return Ok("Posted Successfully");
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteFeedback(int id)
        {
            var FeedbackToBeDeleted = db.feedback.Find(id);
            if (FeedbackToBeDeleted == null)
            {
                return NotFound($"Feedback with id {id} is not found");
            }
            db.feedback.Remove(FeedbackToBeDeleted);
            db.SaveChanges();
            return Ok("deleted Successfully");
        }
    }
}
