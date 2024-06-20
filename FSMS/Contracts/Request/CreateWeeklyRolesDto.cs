namespace FSMS.Contracts.Request
{
    public record CreateWeeklyRolesDto
    {
        public Guid EmployeeId { get; set; }
        public List<string>? Roles { get; set; }
    }
}
