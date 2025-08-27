

using ELearnApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkController : ControllerBase
    {
        private readonly ELearnDbContext db;

        public BookMarkController(ELearnDbContext context)
        {
            db = context;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookMark(long id)
        {
            var bookmarkToBeDeleted = await db.BookMarks.FindAsync(id);
            if (bookmarkToBeDeleted == null)
                return NotFound($"Bookmark with id {id} not found");

            db.BookMarks.Remove(bookmarkToBeDeleted);
            await db.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }


        // GET all bookmarks
        [HttpGet]
        public async Task<IActionResult> GetBookmarks()
        {
            var bookmarks = await db.BookMarks
                .Include(b => b.Card)   // include card details
                .Select(b => new {
                    id = b.Id,
                    cardId = b.CardId,
                    question = b.Card.Question,
                    answer = b.Card.Answer
                })
                .ToListAsync();

            return Ok(bookmarks);
        }


       
        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleBookmark([FromBody] BookmarkDTO dto)
        {
            var existing = await db.BookMarks.FirstOrDefaultAsync(b => b.CardId == dto.CardId);

            if (existing != null)
            {
                db.BookMarks.Remove(existing);
                await db.SaveChangesAsync();
                return Ok(new { isBookmarked = false, id = (int?)null });
            }

            var bookmark = new BookMarks { CardId = dto.CardId };
            db.BookMarks.Add(bookmark);
            await db.SaveChangesAsync();

            // Fetch full card details
            var card = await db.Cards.FindAsync(dto.CardId);

            return Ok(new
            {
                isBookmarked = true,
                id = bookmark.Id,
                cardId = card.Id,
                question = card.Question,
                answer = card.Answer
            });
        }
    }
}

