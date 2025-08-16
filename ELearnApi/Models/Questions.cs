namespace ELearnApi.Models
{
    public class Questions
    {

        public long Id { get; set; }                 
        public string Question { get; set; }        
        public string Answer { get; set; }
        public bool Favorite { get; set; }           
        public bool IsReviewed { get; set; }          
        public string BookmarkEntryId { get; set; }
    }
}
