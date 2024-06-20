using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Entities;
using FSMS.Extensions;
using FSMS.Features.Dispensers;
using MediatR;

namespace FSMS.Features.Dispensers
{
    public abstract class CreateDispenser
    {
        public class Command : IRequest<Result>
        {
            public string Identifier { get; set; } = string.Empty;
        }

        public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var dispenser = new Dispenser
                {
                    Identifier = request.Identifier
                };

                context.Dispensers.Add(dispenser);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class CreateDispenserEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/dispenser", async (CreateDispenserDto dispenser, ISender sender) =>
        {
            var request = new CreateDispenser.Command
            {
                Identifier = dispenser.Identifier
            };

            var result = await sender.Send(request);

            return Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Dispenser");
    }
}
