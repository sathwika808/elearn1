using System.ComponentModel.DataAnnotations;

namespace ELearnApi.Models
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; }


        public string Description { get; set; }

        public string VideoUrl { get; set; }

        // Navigation properties

        public ICollection<Card> Cards { get; set; }
    }

}
