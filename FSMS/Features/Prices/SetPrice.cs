using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Entities;
using FSMS.Extensions;
using FSMS.Features.Prices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Prices
{
    public abstract class SetPrice
    {
        public class Command : IRequest<Result>
        {
            public decimal Value { get; set; }
            public FuelType Fuel { get; set; }
        }

        public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var lastPrice = await context.Prices
                    .OrderByDescending(p => p.StartDate)
                    .FirstOrDefaultAsync(cancellationToken);

                if (lastPrice != null)
                {
                    lastPrice.EndDate = DateTime.UtcNow;
                    lastPrice.Validity = DateTime.Today.Subtract(lastPrice.StartDate).Days;
                }

                var prices = new Price
                {
                    Value = request.Value,
                    Fuel = request.Fuel,
                    StartDate = DateTime.UtcNow
                };

                context.Prices.Add(prices);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreatePriceEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/price", async (CreatePriceDto price, ISender sender) =>
        {
            var request = new SetPrice.Command
            {
                Value = price.Value,
                Fuel = price.Fuel
            };

            var result = await sender.Send(request);

            return Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Price");
    }
}