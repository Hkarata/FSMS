using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Tanks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Tanks
{
    public abstract class GetTanks
    {
        public class Query : IRequest<Result<List<TankDto>>>
        {
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, Result<List<TankDto>>>
        {
            public async Task<Result<List<TankDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tanks = await context.Tanks
                    .AsNoTracking()
                    .Select(tank => new TankDto
                    {
                        Id = tank.Id,
                        Name = tank.Identifier,
                        Fuel = tank.Fuel,
                        Capacity = tank.Capacity
                    })
                    .ToListAsync(cancellationToken);

                if (tanks.Count == 0)
                {
                    return Result.Failure<List<TankDto>>(Error.NullValue);
                }

                return Result.Success(tanks);
            }
        }
    }
}

public class GetTanksEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tanks", async (ISender sender) =>
        {
            var request = new GetTanks.Query();
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<List<TankDto>>>()
            .WithTags("Tank");
    }
}