using System.ComponentModel.DataAnnotations.Schema;

namespace ELearnApi.Models
{
     
    public class BookMarks
    {

    public int Id { get; set; }
     
    public string CardId { get; set; }
        public string question { get; set; }
    public string answer { get; set; }
    }
}
