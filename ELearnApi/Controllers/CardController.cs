using ELearnApi.Models;
using Microsoft.AspNetCore.Mvc;

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
                              Id = c.Id,
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
        public IActionResult UpdateCard(long id, CardDTO dto)
        {
            var card = db.Cards.Find(id);
            if (card == null) return NotFound($"Card {id} not found");

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
        public IActionResult DeleteCard(long id)
        {
            var card = db.Cards.Find(id);
            if (card == null) return NotFound($"Card {id} not found");

            db.Cards.Remove(card);
            db.SaveChanges();

            return Ok("Card deleted successfully");
        }
    }
}
    