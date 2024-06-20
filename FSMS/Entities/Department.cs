namespace FSMS.Entities
{
    public class Department
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;

        // Safe Delete
        public bool IsDeleted { get; set; }

        // Navigation properties
        public List<Employee>? Employees { get; set; }
    }
}
