using ELearnApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ELearnDbContext db;

        public CardController(ELearnDbContext context)
        {
            db = context;
        }


        [HttpPut("{courseId}/Card/{cardId}/review")]
        public IActionResult MarkCardAsReviewed(int courseId, int cardId)
        {
            var course = db.Courses
                .Include(c => c.Cards)
                .FirstOrDefault(c => c.CourseId == courseId);

            if (course == null)
                return NotFound($"Course {courseId} not found");

            var card = course.Cards.FirstOrDefault(c => c.Id == cardId);
            if (card == null)
                return NotFound($"Card {cardId} not found in Course {courseId}");

            card.IsReviewed = true;
            db.SaveChanges();

            return Ok(new { cardId = card.Id, isReviewed = card.IsReviewed });
        }


        // GET all cards
        [HttpGet]
        public IActionResult GetCards()
        {
            var cards = db.Cards.ToList();
            return Ok(cards);
        }

        // GET card by id
        [HttpGet("{id}")]
        public IActionResult GetCard(int id)
        {
            var card = db.Cards.Find(id);
            if (card == null) return NotFound($"Card {id} not found");
            return Ok(card);
        }

        [HttpGet("byCourse/{courseId}")]
        public IActionResult GetCardsByCourse(int courseId)
        {
            var cards = db.Cards
                          .Where(c => c.CourseId == courseId)
                          .Select(c => new CardDTO
                          {
                              Id =(int ) c.Id,
                              Question = c.Question,
                              Answer = c.Answer,
                              Favorite = c.Favorite,
                              IsReviewed = c.IsReviewed,
                              CourseId = c.CourseId
                          }).ToList();

            if (!cards.Any())
                return NotFound($"No cards found for course id {courseId}");

            return Ok(cards);
        }



            [HttpPost]

        public IActionResult PostCard(CardDTO dto)
        {
            var course = db.Courses.Find(dto.CourseId);
            if (course == null)
                return NotFound($"Course with id {dto.CourseId} not found");

            var card = new Card
            {
                Id = dto.Id,
                Question = dto.Question,
                Answer = dto.Answer,
                Favorite = dto.Favorite,
                IsReviewed = dto.IsReviewed,
                CourseId = dto.CourseId
            };

            db.Cards.Add(card);
            db.SaveChanges();

            return Ok("Card added successfully");
        }



        // PUT update card
        [HttpPut("{id}")]
        public IActionResult UpdateCard(int id, CardDTO dto)
        {
            var card = db.Cards.Find(id);
            if (card == null) return NotFound($"Card {id} not found");
            card.Id = dto.Id;
            card.Question = dto.Question;
            card.Answer = dto.Answer;
            card.Favorite = dto.Favorite;
            card.IsReviewed = dto.IsReviewed;
            card.CourseId = dto.CourseId;

            db.SaveChanges();

            return Ok("Card updated successfully");
        }

        // DELETE card
        [HttpDelete("{id}")]
        public IActionResult DeleteCard(int id)
        {
            var card = db.Cards.Find(id);
            if (card == null) return NotFound($"Card {id} not found");

            db.Cards.Remove(card);
            db.SaveChanges();

            return Ok("Card deleted successfully");
        }
    }
}
    