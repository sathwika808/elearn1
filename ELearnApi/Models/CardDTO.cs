using System.ComponentModel.DataAnnotations;

namespace ELearnApi.Models
{
    public class CardDTO
    {
        
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool Favorite { get; set; }
        public bool IsReviewed { get; set; }
      

        // Only FK to link card → course
        public int CourseId { get; set; }
    }
}
