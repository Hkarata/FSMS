using Carter;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Departments;
using MediatR;

namespace FSMS.Features.Departments
{
	public abstract class DeleteDepartment
	{
		public class Command : IRequest<Result>
		{
			public Guid Id { get; set; }
		}

		public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result>
		{
			public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
			{
				var department = await context.Departments.FindAsync(request.Id);

				if (department == null)
				{
					return Result.Failure(Error.NonExistentDepartment);
				}

				context.Departments.Remove(department);

				await context.SaveChangesAsync(cancellationToken);

				return Result.Success();
			}
		}
	}
}

public class DeleteDepartmentEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/api/department/{id:guid}", async (Guid id, ISender sender) =>
		{
			var request = new DeleteDepartment.Command
			{
				Id = id
			};

			var result = await sender.Send(request);

			return result.IsSuccess ? Results.Ok() : Results.Ok(result.Error);
		})
			.Produces<Result>()
			.WithTags("Department");
	}
}
