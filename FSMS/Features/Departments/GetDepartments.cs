using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Departments
{
    public abstract class GetDepartments
    {
        public class Query : IRequest<Result<List<DepartmentDto>>>;

        public sealed class Handler(AppDbContext context) : IRequestHandler<Query, Result<List<DepartmentDto>>>
        {
            public async Task<Result<List<DepartmentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var departments = await context.Departments
                    .AsNoTracking()
                    .Select(d => new DepartmentDto
                    {
                        Id = d.Id,
                        Name = d.Name
                    })
                    .ToListAsync(cancellationToken);

                if (departments.Count == 0)
                {

                    return Result.Failure<List<DepartmentDto>>(Error.NullValue);
                }

                return departments;
            }
        }
    }
}

public class GetDepartmentsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/departments", async (ISender sender) =>
        {
            var request = new GetDepartments.Query();

            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<List<DepartmentDto>>>()
            .WithTags("Department");
    }
}