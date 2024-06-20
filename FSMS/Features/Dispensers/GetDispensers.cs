using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Dispensers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Dispensers
{
    public abstract class GetDispensers
    {
        public class Query : IRequest<Result<List<DispenserDto>>>;

        public sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<List<DispenserDto>>>
        {
            public async Task<Result<List<DispenserDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dispensers = await context.Dispensers
                    .AsNoTracking()
                    .Select(dispenser => new DispenserDto
                    {
                        Id = dispenser.Id,
                        Name = dispenser.Identifier,
                    })
                    .ToListAsync(cancellationToken);

                if (dispensers.Count == 0)
                {
                    return Result.Failure<List<DispenserDto>>(Error.NullValue);
                }

                return dispensers;
            }
        }
    }
}


public class GetDispensersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dispensers", async (ISender sender) =>
        {
            var request = new GetDispensers.Query();
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<List<DispenserDto>>>()
            .WithTags("Dispenser");
    }
}