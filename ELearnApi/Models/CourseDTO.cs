namespace ELearnApi.Models
{
    public class CourseDTO
    {
        public int CourseId { get; set; }   // PK
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public List<CardDTO>? Cards { get; set; }
    }
}
