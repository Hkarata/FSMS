using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Employees
{
	public abstract class GetEmployee
	{
		public class Command : IRequest<Result<EmployeeDto>>
		{
			public Guid Id { get; set; }
		}

		public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result<EmployeeDto>>
		{
			public async Task<Result<EmployeeDto>> Handle(Command request, CancellationToken cancellationToken)
			{
				var employee = await context.Employees
					.Include(e => e.Department)
					.Where(e => e.Id == request.Id)
					.Select(e => new EmployeeDto
					{
						Id = e.Id,
						FirstName = e.FirstName,
						LastName = e.LastName,
						Email = e.Email,
						PhoneNumber = e.PhoneNumber,
						Department = e.Department!.Name
					})
					.FirstOrDefaultAsync(cancellationToken);

				if (employee is null)
				{
					return Result.Failure<EmployeeDto>(Error.NullValue);
				}

				return employee;
			}
		}
	}
}

public class GetEmployeeEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/employee/{id:guid}", async (Guid id, ISender sender) =>
		{
			var request = new GetEmployee.Command
			{
				Id = id
			};

			var result = await sender.Send(request);

			return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
		})
			.Produces<Result<EmployeeDto>>()
			.WithTags("Employee");
	}
}