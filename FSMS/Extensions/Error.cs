namespace FSMS.Extensions
{
	public record Error(string Code, string Message)
	{
		public static readonly Error None = new(string.Empty, string.Empty);

		public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

		public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.");

		public static readonly Error NonExistentTank = new("Error.NonExistentTank", "The specified tank does not exist.");

		public static readonly Error NonExistentDepartment = new("Error.NonExistentDepartment", "The specified department does not exist.");

		public static readonly Error NonExistentEmployee = new("Error.NonExistentEmployee", "The specified employee does not exist.");

		public static readonly Error NonExistentDispenser = new("Error.NonExistentDispenser", "The specified dispenser does not exist.");

		public static readonly Error NonExistentAllocation = new("Error.NonExistentAllocation", "The specified allocation does not exist.");
	}
}
