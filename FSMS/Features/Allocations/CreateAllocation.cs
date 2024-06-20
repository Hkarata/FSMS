using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Entities;
using FSMS.Extensions;
using FSMS.Features.Allocations;
using MediatR;

namespace FSMS.Features.Allocations
{
    public abstract class CreateAllocation
    {
        public class Command : IRequest<Result>
        {
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public Guid EmployeeId { get; set; }
            public Guid DispenserId { get; set; }
        }

        public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var allocation = new Allocation
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    EmployeeId = request.EmployeeId,
                    DispenserId = request.DispenserId
                };

                context.Allocations.Add(allocation);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreateAllocationEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/allocation", async (CreateAllocationDto allocation, ISender sender) =>
        {
            var request = new CreateAllocation.Command
            {
                StartDate = allocation.StartDate,
                EndDate = allocation.EndDate,
                EmployeeId = allocation.EmployeeId,
                DispenserId = allocation.DispenserId
            };

            var result = await sender.Send(request);

            return Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Allocation");
    }
}