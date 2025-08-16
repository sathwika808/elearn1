using ELearnApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkController : ControllerBase
    {
        ELearnDbContext db = null;
        public BookMarkController(ELearnDbContext c)
        {
            db= c;  

        }


        [HttpGet]

        public IActionResult GetBookMark()
        {
            var BookMarks = db.BookMarks.ToList();
            if (BookMarks == null)
            {
                return NotFound("No Feedbacks");
            }
            return Ok(BookMarks);
        }



        [HttpGet("{id}")]

        public IActionResult GetBookMarkById(int id)
        {
            var Bookmarks = db.BookMarks.Find(id);
            if (Bookmarks == null)
            {
                return NotFound($"Feedback with {id} is not found");
            }
            return Ok(Bookmarks);
        }



        [HttpPost]
        public IActionResult PostBookMark(BookMarks postedBookmark)
        {
           if(postedBookmark == null)            {
                return BadRequest("post valid  bookmark"); 
            }
            db.BookMarks.Add(postedBookmark);
            db.SaveChanges();
            return Ok("Posted succesfully");
            
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteFeedback(int id)
        {
            var BookmarkToBeDeleted = db.feedback.Find(id);
            if (BookmarkToBeDeleted == null)
            {
                return NotFound($"Feedback with id {id} is not found");
            }
            db.feedback.Remove(BookmarkToBeDeleted);
            db.SaveChanges();
            return Ok("deleted Successfully");
        }


    }
}
