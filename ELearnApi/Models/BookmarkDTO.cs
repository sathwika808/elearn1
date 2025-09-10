using System.Text.Json.Serialization;

namespace ELearnApi.Models
{
    public class BookmarkDTO
    {
        [JsonPropertyName("cardId")]   
        public int CardId { get; set; }
    }
}
