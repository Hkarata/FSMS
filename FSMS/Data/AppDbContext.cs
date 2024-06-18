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
			modelBuilder.Entity<Department>().HasData(
				new Department { Id = Guid.NewGuid(), Name = "Accounts" },
				new Department { Id = Guid.NewGuid(), Name = "HR" },
				new Department { Id = Guid.NewGuid(), Name = "Marketing" },
				new Department { Id = Guid.NewGuid(), Name = "Sales" },
				new Department { Id = Guid.NewGuid(), Name = "Procurement & Maintenance" }
			);

			base.OnModelCreating(modelBuilder);
		}
	}
}
