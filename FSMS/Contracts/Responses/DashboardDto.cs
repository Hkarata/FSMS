namespace FSMS.Contracts.Responses
{
    public record DashboardDto
    {
        public int Dispensers { get; init; }
        public int Tanks { get; init; }
        public int Employees { get; init; }
    }
}
