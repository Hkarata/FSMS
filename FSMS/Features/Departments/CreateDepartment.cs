using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Entities;
using FSMS.Extensions;
using FSMS.Features.Departments;
using MediatR;

namespace FSMS.Features.Departments
{
	public abstract class CreateDepartment
	{
		public record Command(string Name) : IRequest<Result<Guid>>;

		public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Guid>>
		{
			public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
			{
				var department = new Department
				{
					Name = request.Name
				};

				context.Departments.Add(department);
				await context.SaveChangesAsync(cancellationToken);

				return department.Id;
			}
		}
	}
}

public class CreateDepartmentEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/department", async (CreateDepartmentDto department, ISender sender) =>
		{
			var request = new CreateDepartment.Command(department.Name);

			var result = await sender.Send(request);

			return Results.Ok(result);

		})
			.Produces<Result<Guid>>()
			.WithTags("Employee");
	}
}