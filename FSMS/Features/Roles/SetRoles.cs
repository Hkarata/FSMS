using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Entities;
using FSMS.Extensions;
using FSMS.Features.Roles;
using MediatR;

namespace FSMS.Features.Roles
{
    public abstract class SetRoles
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
                var weeklyRole = new WeeklyRole
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = request.EmployeeId,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(7),
                    Roles = request.Roles!.Select(role => new Role
                    {
                        Name = role
                    }).ToList()
                };

                await context.WeeklyRoles.AddAsync(weeklyRole);

                await context.SaveChangesAsync();

                return Result.Success();
            }
        }
    }
}

public class SetRolesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/roles", async (CreateWeeklyRolesDto roles, ISender sender) =>
        {
            var request = new SetRoles.Command
            {
                EmployeeId = roles.EmployeeId,
                Roles = roles.Roles
            };

            var result = await sender.Send(request);

            return Results.Ok(result);
        })
            .Produces<Result>()
            .WithTags("Weekly Roles");
    }
}
