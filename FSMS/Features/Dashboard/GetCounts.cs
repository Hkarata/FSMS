using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Dashboard;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Dashboard
{
    public abstract class GetCounts
    {
        public class Query : IRequest<Result<DashboardDto>>;

        public sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<DashboardDto>>
        {
            public async Task<Result<DashboardDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dispensers = await context.Dispensers
                    .AsNoTracking()
                    .CountAsync(cancellationToken);

                var tanks = await context.Tanks
                    .AsNoTracking()
                    .CountAsync(cancellationToken);

                var employees = await context.Employees
                    .AsNoTracking()
                    .CountAsync(cancellationToken);

                return new DashboardDto
                {
                    Dispensers = dispensers,
                    Tanks = tanks,
                    Employees = employees,
                };
            }
        }
    }
}

public class GetCountsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dashboard", async (ISender sender) =>
        {
            var request = new GetCounts.Query();
            var result = await sender.Send(request);
            return Results.Ok(result);
        })
            .Produces<Result<DashboardDto>>()
            .WithTags("Dashboard");
    }
}
