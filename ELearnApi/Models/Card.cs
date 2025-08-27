
using System.ComponentModel.DataAnnotations;

namespace ELearnApi.Models
{
    public class Card
    {
        [Key]
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool Favorite { get; set; }
        public bool IsReviewed { get; set; }
        public int CourseId { get; set; }   // Foreign key
        public Courses Course { get; set; }   // Navigation property
    }
}
