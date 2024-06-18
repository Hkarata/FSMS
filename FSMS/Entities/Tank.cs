namespace FSMS.Entities
{
	public class Tank
	{
		public Guid Id { get; init; }
		public string Identifier { get; set; } = string.Empty;
		public double Capacity { get; set; }
		public bool IsActive { get; set; }
	}
}
