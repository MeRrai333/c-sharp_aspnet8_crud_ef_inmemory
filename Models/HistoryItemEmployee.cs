namespace dotnet_crud.Models
{
    public class HistoryItemEmployee
    { 
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int EmployeeId { get; set; }
        public int Qty { get; set; }
        public DateTime CreateAt { get; private set; }

        public HistoryItemEmployee(){
            this.CreateAt = DateTime.Now;
        }
    }
}