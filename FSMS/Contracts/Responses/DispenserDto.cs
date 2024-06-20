namespace FSMS.Contracts.Responses
{
    public record DispenserDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
