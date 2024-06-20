namespace FSMS.Entities
{
    public class Employee
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        // Safe Delete
        public bool IsDeleted { get; set; }

        // Navigation properties
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Allocation>? Allocations { get; set; }
        public List<WeeklyRole>? WeeklyRoles { get; set; }
    }
}
