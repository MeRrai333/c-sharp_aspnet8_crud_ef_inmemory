using System.Text.Json.Serialization;

namespace dotnet_crud.Models
{
    public class HistoryItemEmployee
    { 
        public int Id { get; set; }
        public int ItemId { get; set; }
        [JsonIgnore]
        public Item Item { get; set; } = new Item();
        public int EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; } = new Employee();
        public int Qty { get; set; }
        public DateTime BorrowTime { get; private set; }
        public DateTime? ReturnTime { get; set; }



        public HistoryItemEmployee(){
            this.BorrowTime = DateTime.Now;
        }
    }
}