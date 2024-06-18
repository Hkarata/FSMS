using FSMS.Entities;

namespace FSMS.Contracts.Request
{
	public class CreateAllocationDto
	{
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public Guid EmployeeId { get; set; }
		public Guid DispenserId { get; set; }
	}
}
