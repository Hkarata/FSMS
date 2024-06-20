using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Allocations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Allocations
{
	public abstract class GetAllocations
	{
		public class Command : IRequest<Result<List<AllocationDto>>>
		{
			public int Id { get; set; }
		}

		public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result<List<AllocationDto>>>
		{
			public async Task<Result<List<AllocationDto>>> Handle(Command request, CancellationToken cancellationToken)
			{
				var query = await context.Allocations
					.AsNoTracking()
					.Include(a => a.Employee)
					.Include(a => a.Dispenser)
					.GroupBy(a => a.EmployeeId)
					.Select(g => g.OrderByDescending(a => a.StartDate).FirstOrDefault())
					.ToListAsync(cancellationToken);

				if (query.Count == 0)
				{
					return Result.Failure<List<AllocationDto>>(Error.None);
				}

				var allocations = query.Select(a => new AllocationDto
				{
					Id = a!.Id,
					StartDate = a.StartDate,
					EndDate = a.EndDate,
					EmployeeName = a.Employee!.FirstName + " " + a.Employee.LastName,
					Dispenser = a.Dispenser!.Identifier
				}).ToList();

				return allocations;
			}
		}
	}
}


public class GetAllocationsEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/allocations", async (ISender sender) =>
		{
			var request = new GetAllocations.Command();
			var result = await sender.Send(request);
			return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
		})
			.Produces<Result<List<AllocationDto>>>()
			.WithTags("Allocation");
	}
}