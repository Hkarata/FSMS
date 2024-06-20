using FSMS.Data;

namespace FSMS.Contracts.Request
{
	public record CreateTankDto
	{
		public string Identifier { get; set; } = string.Empty;
		public double Capacity { get; set; }
        public FuelType Fuel { get; set; }
    }
}
