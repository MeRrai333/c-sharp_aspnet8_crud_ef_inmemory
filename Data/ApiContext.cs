using dotnet_crud.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_crud.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<HistoryItemEmployee> HistoryItemEmployees { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) {

        }
    }
}