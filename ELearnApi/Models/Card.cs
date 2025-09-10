
using System.ComponentModel.DataAnnotations;

namespace ELearnApi.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool Favorite { get; set; }
        public bool IsReviewed { get; set; } = false;
        public int CourseId { get; set; }   // Foreign key
        public Courses Course { get; set; }   // Navigation property                                                      

    }
}
