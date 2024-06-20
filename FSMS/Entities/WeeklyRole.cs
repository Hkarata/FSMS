namespace FSMS.Entities
{
    public class WeeklyRole
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // Navigation properties
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public List<Role>? Roles { get; set; }
    }
}
