namespace FSMS.Contracts.Responses
{
	public record TankDto
	{
		public Guid Id { get; init; }
		public string Name { get; init; } = string.Empty;
		public double Capacity { get; init; }
	}
}
