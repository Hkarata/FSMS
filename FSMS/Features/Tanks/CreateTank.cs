using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Entities;
using FSMS.Extensions;
using FSMS.Features.Tanks;
using MediatR;

namespace FSMS.Features.Tanks
{
	public abstract class CreateTank
	{
		public class Command : IRequest<Result<Guid>>
		{
			public string Identifier { get; set; } = string.Empty;
			public double Capacity { get; set; }
		}

		public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result<Guid>>
		{

			public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
			{
				var tank = new Tank
				{
					Id = Guid.NewGuid(),
					Identifier = request.Identifier,
					Capacity = request.Capacity,
					IsActive = true
				};

				context.Tanks.Add(tank);
				await context.SaveChangesAsync(cancellationToken);

				return Result<Guid>.Success(tank.Id);
			}
		}

	}
}

public class CreateTankEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/tank", async (CreateTankDto tank,ISender sender) =>
		{
			var request = new CreateTank.Command
			{
				Identifier = tank.Identifier,
				Capacity = tank.Capacity
			};

			var result = await sender.Send(request);

			return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result.Error);
		});
	}
}