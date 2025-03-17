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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HistoryItemEmployee>()
                .HasKey(h => h.Id);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.HistoryItems)
                .WithOne(h => h.Employee)
                .HasForeignKey(h => h.EmployeeId);
            modelBuilder.Entity<Item>()
                .HasMany(e => e.HistoryItems)
                .WithOne(h => h.Item)
                .HasForeignKey(h => h.ItemId);
        }
    }
}