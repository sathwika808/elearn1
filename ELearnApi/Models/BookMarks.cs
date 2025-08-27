using System.ComponentModel.DataAnnotations.Schema;

namespace ELearnApi.Models
{

    public class BookMarks
    {

        public int Id { get; set; }

        public long CardId { get; set; }
       
        public Card Card { get; set; }
    }
}
