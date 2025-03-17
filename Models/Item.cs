using System.Text.Json.Serialization;

namespace dotnet_crud.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public int? Qty {get; set;}
        [JsonIgnore]
        public List<HistoryItemEmployee>? HistoryItems { get; set; } = new List<HistoryItemEmployee>();
    }
}