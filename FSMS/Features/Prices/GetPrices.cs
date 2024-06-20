using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Prices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Prices
{
    public abstract class GetPrices
    {
        public class Query : IRequest<Result<List<PriceDto>>>
        {
            public FuelType Fuel { get; set; }
        }

        public sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<List<PriceDto>>>
        {
            public async Task<Result<List<PriceDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var prices = await context.Prices
                    .OrderByDescending(p => p.StartDate)
                    .Where(p => p.Fuel == request.Fuel)
                    .Select(p => new PriceDto
                    {
                        Id = p.Id,
                        Value = p.Value,
                        Fuel = p.Fuel,
                        StartDate = p.StartDate,
                        EndDate = p.EndDate,
                        Validity = DateTime.Today.Subtract(p.StartDate).Days
                    })
                    .ToListAsync(cancellationToken);

                if (prices is null)
                {
                    return Result.Failure<List<PriceDto>>(Error.NullValue);
                }

                return prices;
            }
        }

    }
}

public class GetPricesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/prices/{fuel:int}", async (int fuel, ISender sender) =>
        {
            var request = new GetPrices.Query { Fuel = (FuelType)fuel };
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<List<PriceDto>>>()
            .WithTags("Price");
    }
}