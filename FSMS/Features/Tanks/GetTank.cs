using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Tanks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Tanks
{
	public abstract class GetTank
	{
		public class Query : IRequest<Result<TankDto>>
		{
			public Guid Id { get; set; }
		}

		public class Handler(AppDbContext context) : IRequestHandler<Query, Result<TankDto>>
		{
			public async Task<Result<TankDto>> Handle(Query request, CancellationToken cancellationToken)
			{
				var tank = await context.Tanks
					.AsNoTracking()
					.Where(t => t.Id == request.Id)
					.SingleOrDefaultAsync(cancellationToken);

				if (tank is null)
				{
					return Result.Failure<TankDto>(Error.NonExistentTank);
				}

				return Result.Success(new TankDto
				{
					Id = tank.Id,
					Name = tank.Identifier,
                    Fuel = tank.Fuel,
                    Capacity = tank.Capacity
				});
			}
		}
	}
}

public class GetTankEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/tanks/{id}", async (Guid id, ISender sender) =>
		{
			var request = new GetTank.Query { Id = id };
			var result = await sender.Send(request);
			return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
		})
			.Produces<Result<TankDto>>()
			.WithTags("Tank");
	}
}
