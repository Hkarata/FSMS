namespace FSMS.Contracts.Responses
{
    public record DepartmentDto
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public List<EmployeeDto>? Employees { get; set; }
    }
}
