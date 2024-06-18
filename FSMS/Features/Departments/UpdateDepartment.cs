using Carter;
using FSMS.Contracts.Request;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Departments;
using MediatR;

namespace FSMS.Features.Departments
{
	public abstract class UpdateDepartment
	{
		public class Command : IRequest<Result>
		{
			public Guid Id { get; set; }
			public string Name { get; set; } = string.Empty;
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

				department.Name = request.Name;

				await context.SaveChangesAsync(cancellationToken);

				return Result.Success();
			}
		}
	}
}

public class UpdateDepartmentEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/api/department/{id:guid}", async (Guid id, CreateDepartmentDto department, ISender sender) =>
		{
			var request = new UpdateDepartment.Command
			{
				Id = id,
				Name = department.Name
			};

			var result = await sender.Send(request);

			return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
		})
			.Produces<Result>()
			.WithTags("Department");
	}
}
