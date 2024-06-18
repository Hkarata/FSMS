namespace FSMS.Contracts.Request
{
	public record CreateTankDto
	{
		public string Identifier { get; set; } = string.Empty;
		public double Capacity { get; set; }
	}
}
