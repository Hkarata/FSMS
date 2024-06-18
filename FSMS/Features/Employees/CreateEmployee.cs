using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Entities;
using FSMS.Extensions;
using FSMS.Features.Employees;
using MediatR;

namespace FSMS.Features.Employees
{
	public abstract class CreateEmployee
	{
		public class Command : IRequest<Result<Guid>>
		{
			public string FirstName { get; set; } = string.Empty;
			public string LastName { get; set; } = string.Empty;
			public string Email { get; set; } = string.Empty;
			public string PhoneNumber { get; set; } = string.Empty;
			public Guid DepartmentId { get; set; }
		}

		public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result<Guid>>
		{
			public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
			{
				var employee = new Employee
				{
					Id = Guid.NewGuid(),
					FirstName = request.FirstName,
					LastName = request.LastName,
					Email = request.Email,
					PhoneNumber = request.PhoneNumber,
					DepartmentId = request.DepartmentId
				};

				context.Employees.Add(employee);
				await context.SaveChangesAsync(cancellationToken);

				return employee.Id;
			}
		}
	}
}

public class CreateEmployeeEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/employee", async (CreateEmployeeDto employee, ISender sender) =>
		{
			var request = new CreateEmployee.Command
			{
				FirstName = employee.FirstName,
				LastName = employee.LastName,
				Email = employee.Email,
				PhoneNumber = employee.PhoneNumber,
				DepartmentId = employee.DepartmentId
			};

			var result = await sender.Send(request);

			return Results.Ok(result);
		})
			.Produces<Result<Guid>>()
			.WithTags("Employee");
	}
}