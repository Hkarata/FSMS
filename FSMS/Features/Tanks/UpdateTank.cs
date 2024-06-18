using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Tanks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Tanks
{
	public abstract class UpdateTank
	{
		public class Command : IRequest<Result>
		{
			public Guid Id { get; set; }
			public string Name { get; set; } = string.Empty;
			public double Capacity { get; set; }
		}

		public class Handler(AppDbContext context) : IRequestHandler<Command, Result>
		{
			public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
			{
				var tank = await context.Tanks
					.Where(t => t.Id == request.Id)
					.SingleOrDefaultAsync(cancellationToken);

				if (tank is null)
				{
					return Result.Failure(Error.NonExistentTank);
				}

				tank.Identifier = request.Name;
				tank.Capacity = request.Capacity;

				await context.SaveChangesAsync(cancellationToken);

				return Result.Success();
			}
		}
	}
}

public class UpdateTankEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/api/tanks/{id:guid}", async (Guid id, CreateTankDto tank, ISender sender) =>
		{
			var request = new UpdateTank.Command
			{
				Id = id,
				Name = tank.Identifier,
				Capacity = tank.Capacity
			};

			var result = await sender.Send(request);

			return result.IsSuccess ? Results.Ok() : Results.Ok(result.Error);
		});
	}
}
