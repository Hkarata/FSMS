using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Entities;
using FSMS.Extensions;
using FSMS.Features.Roles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Roles
{
    public abstract class UpdateRoles
    {
        public class Command : IRequest<Result>
        {
            public Guid EmployeeId { get; set; }
            public List<string>? Roles { get; set; }
        }

        public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var weeklyRole = await context.WeeklyRoles
                    .Include(weeklyRole => weeklyRole.Roles)
                    .Where(weeklyRole => weeklyRole.EmployeeId == request.EmployeeId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (weeklyRole is null)
                {
                    return Result.Failure(Error.NullValue);
                }

                weeklyRole.Roles = request.Roles!.Select(role => new Role
                {
                    Name = role
                }).ToList();

                await context.SaveChangesAsync();

                return Result.Success();
            }
        }
    }
}

public class UpdateRolesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/roles/{weeklyRoleId:guid}", async (CreateWeeklyRolesDto roles, ISender sender) =>
        {
            var request = new UpdateRoles.Command
            {
                EmployeeId = roles.EmployeeId,
                Roles = roles.Roles
            };

            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Roles");
    }
}