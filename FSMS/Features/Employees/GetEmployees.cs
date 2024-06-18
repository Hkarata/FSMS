using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Employees
{
	public abstract class GetEmployees
	{
		public class Query : IRequest<Result<List<EmployeeDto>>>
		{
		}

		public sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<List<EmployeeDto>>>
		{
			public async Task<Result<List<EmployeeDto>>> Handle(Query request, CancellationToken cancellationToken)
			{
				var employees = await context.Employees
					.Include(x => x.Department)
					.Where(x => !x.IsDeleted)
					.Select(x => new EmployeeDto
					{
						FirstName = x.FirstName,
						LastName = x.LastName,
						Email = x.Email,
						PhoneNumber = x.PhoneNumber,
						Department = x.Department!.Name
					})
					.ToListAsync(cancellationToken);

				if (employees.Count == 0)
				{
					return Result.Failure<List<EmployeeDto>>(Error.NullValue);
				}

				return employees;
			}
		}
	}
}

public class GetEmployeesEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/employees", async (ISender sender) =>
		{
			var request = new GetEmployees.Query();

			var result = await sender.Send(request);

			return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
		})
			.Produces<Result<List<EmployeeDto>>>()
			.WithTags("Employee");
	}
}