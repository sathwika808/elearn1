namespace ELearnApi.Models
{
    public class CourseDTO
    {
        public int CourseId { get; set; }   // PK
        public string Title { get; set; }
        public string Description { get; set; } 
    
        // If you want to return related cards, you can include them here
        public List<CardDTO>? Cards { get; set; }
    }
}
