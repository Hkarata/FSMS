using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Allocations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Allocations
{
	public class GetEmployeeAllocations
	{
		public class Query : IRequest<Result<List<AllocationDto>>>
		{
			public Guid EmployeeId { get; set; }
		}

		public sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<List<AllocationDto>>>
		{
			public async Task<Result<List<AllocationDto>>> Handle(Query request, CancellationToken cancellationToken)
			{
				var allocations = await context.Allocations
					.AsNoTracking()
					.Where(x => x.EmployeeId == request.EmployeeId)
					.Select(x => new AllocationDto
					{
						Id = x.Id,
						StartDate = x.StartDate,
						EndDate = x.EndDate
					})
					.ToListAsync(cancellationToken);

				if (allocations is null)
				{
					return Result.Failure<List<AllocationDto>>(Error.None);
				}

				return allocations;
			}
		}
	}
}

public class GetEmployeeAllocationsEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/allocations/{employeeId:guid}", async (Guid employeeId, ISender sender) =>
		{
			var request = new GetEmployeeAllocations.Query
			{
				EmployeeId = employeeId
			};

			var result = await sender.Send(request);

			return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
		})
			.Produces<Result<List<AllocationDto>>>()
			.WithTags("Employee Allocations");
	}
}
