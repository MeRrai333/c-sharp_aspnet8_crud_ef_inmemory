using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_crud.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public String FirstName { get; set; } = String.Empty;
        public String LastName { get; set; } = String.Empty;

        public List<HistoryItemEmployee>? HistoryItems { get; set; } = [];
    }
}