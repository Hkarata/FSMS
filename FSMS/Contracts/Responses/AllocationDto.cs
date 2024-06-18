using FSMS.Entities;

namespace FSMS.Contracts.Responses
{
	public record AllocationDto
	{
		public Guid Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string EmployeeName { get; set; } = string.Empty;
		public string Dispenser { get; set; } = string.Empty;
	}
}
