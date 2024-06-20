using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Prices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Prices
{
    public abstract class GetPrice
    {
        public class Query : IRequest<Result<PriceDto>>
        {
            public FuelType Fuel { get; set; }
        }

        public sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<PriceDto>>
        {
            public async Task<Result<PriceDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var price = await context.Prices
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
                    .FirstOrDefaultAsync(cancellationToken);

                if (price is null)
                {
                    return Result.Failure<PriceDto>(Error.NullValue);
                }

                return price;
            }
        }
    }
}


public class GetPriceEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/price/{fuel:int}", async (int fuel, ISender sender) =>
        {
            var request = new GetPrice.Query { Fuel = (FuelType)fuel };
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<PriceDto>>()
            .WithTags("Price");
    }
}