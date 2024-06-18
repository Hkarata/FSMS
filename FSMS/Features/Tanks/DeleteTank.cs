using Carter;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Tanks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Tanks
{
	public abstract class DeleteTank
	{
		public record Command(Guid Id) : IRequest<Result>;

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

				context.Tanks.Remove(tank);
				await context.SaveChangesAsync(cancellationToken);

				return Result.Success();
			}
		}
	}
}

public class DeleteTankEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/api/tanks/{id:guid}", async (Guid id, ISender sender) =>
		{
			var request = new DeleteTank.Command(id);
			var result = await sender.Send(request);

			return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
		})
			.Produces<Result>()
			.WithTags("Tank");
	}
}
