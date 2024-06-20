using FSMS.Data;

namespace FSMS.Contracts.Responses
{
	public record TankDto
	{
		public Guid Id { get; init; }
		public string Name { get; init; } = string.Empty;
        public FuelType Fuel { get; init; }
        public double Capacity { get; init; }
	}
}
