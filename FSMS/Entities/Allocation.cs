namespace FSMS.Entities
{
    public class Allocation
    {
        public Guid Id { get; init; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public Guid DispenserId { get; set; }
        public Dispenser? Dispenser { get; set; }
    }
}
