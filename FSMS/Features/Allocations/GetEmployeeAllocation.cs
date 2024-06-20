using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Allocations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Allocations
{
    public class GetEmployeeAllocation
    {
        public class Query : IRequest<Result<AllocationDto>>
        {
            public Guid EmployeeId { get; set; }
        }

        public sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<AllocationDto>>
        {
            public async Task<Result<AllocationDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var allocation = await context.Allocations
                    .AsNoTracking()
                    .Include(x => x.Employee)
                    .Include(x => x.Dispenser)
                    .OrderByDescending(x => x.StartDate)
                    .Where(x => x.EmployeeId == request.EmployeeId)
                    .Select(x => new AllocationDto
                    {
                        Id = x.Id,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        EmployeeName = x.Employee!.FirstName + " " + x.Employee.LastName,
                        Dispenser = x.Dispenser!.Identifier
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (allocation is null)
                {
                    return Result.Failure<AllocationDto>(Error.None);
                }

                return allocation;
            }
        }
    }
}

public class GetEmployeeAllocationEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/allocation/{employeeId:guid}", async (Guid employeeId, ISender sender) =>
        {
            var request = new GetEmployeeAllocation.Query
            {
                EmployeeId = employeeId
            };

            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<AllocationDto>>()
            .WithTags("Employee Allocations");
    }
}