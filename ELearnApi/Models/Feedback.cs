namespace ELearnApi.Models
{
    public class Feedback
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Email { get; set; }   
        public string Message { get; set; } 
        public int Rate { get; set; }   
    }
}
