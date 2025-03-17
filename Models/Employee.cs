using System.Text.Json.Serialization;

namespace dotnet_crud.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public String FirstName { get; set; } = String.Empty;
        public String LastName { get; set; } = String.Empty;

        [JsonIgnore]
        public List<HistoryItemEmployee>? HistoryItems { get; set; } = new List<HistoryItemEmployee>();
    }
}