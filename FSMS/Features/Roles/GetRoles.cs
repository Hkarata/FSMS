using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Roles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Roles
{
    public abstract class GetRoles
    {
        public class Query : IRequest<Result<RolesDto>>
        {
            public Guid EmployeeId { get; set; }
        }

        public sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<RolesDto>>
        {
            public async Task<Result<RolesDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var roles = await context.WeeklyRoles
                    .Where(weeklyRole => weeklyRole.EmployeeId == request.EmployeeId)
                    .SelectMany(weeklyRole => weeklyRole.Roles!)
                    .Select(role => role.Name)
                    .ToListAsync(cancellationToken);

                if (roles.Count == 0)
                {
                    return Result.Failure<RolesDto>(Error.NullValue);
                }

                return Result.Success(new RolesDto { Roles = roles });

            }
        }
    }
}

public class GetRolesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/roles/{employeeId:guid}", async (Guid employeeId, ISender sender) =>
        {
            var request = new GetRoles.Query { EmployeeId = employeeId };

            var result = await sender.Send(request);

            return Results.Ok(result);
        })
            .Produces<Result<RolesDto>>()
            .WithTags("Weekly Roles");
    }
}
