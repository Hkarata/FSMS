using Carter;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Employees
{
	public abstract class DeleteEmployee
	{
		public class Command : IRequest<Result>
		{
			public Guid Id { get; set; }
		}

		public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result>
		{
			public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
			{
				var employee = await context.Employees
					.Where(e => e.Id == request.Id)
					.SingleOrDefaultAsync(cancellationToken);

				if (employee is null)
				{
					return Result.Failure(Error.NonExistentEmployee);
				}

				employee.IsDeleted = true;
				await context.SaveChangesAsync(cancellationToken);

				return Result.Success();
			}
		}
	}
}

public class DeleteEmployeeEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/api/employee/{id:guid}", async (Guid id, ISender sender) =>
		{
			var request = new DeleteEmployee.Command
			{
				Id = id
			};

			var result = await sender.Send(request);

			return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
		})
			.Produces<Result>()
			.WithTags("Employee");
	}
}