using FSMS.Entities;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Data
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<Allocation> Allocations { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Dispenser> Dispensers { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Tank> Tanks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
