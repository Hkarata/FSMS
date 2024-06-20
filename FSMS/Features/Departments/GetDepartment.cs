using Carter;
using FSMS.Contracts.Responses;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FSMS.Features.Departments
{
    public abstract class GetDepartment
    {
        public record Query(Guid Id) : IRequest<Result<DepartmentDto>>;

        public class Handler(AppDbContext context) : IRequestHandler<Query, Result<DepartmentDto>>
        {
            public async Task<Result<DepartmentDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var department = await context.Departments
                    .AsNoTracking()
                    .Where(x => x.Id == request.Id && !x.IsDeleted)
                    .Include(x => x.Employees)
                    .Select(x => new DepartmentDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Employees = x.Employees!.Select(e => new EmployeeDto
                        {
                            Id = e.Id,
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            Email = e.Email,
                            PhoneNumber = e.PhoneNumber,
                            Department = x.Name
                        }).ToList()
                    })
                    .SingleOrDefaultAsync(cancellationToken);

                if (department is null)
                {
                    return Result.Failure<DepartmentDto>(Error.NullValue);
                }

                return department;
            }
        }
    }
}

public class GetDepartmentEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/department/{id}", async (Guid id, ISender sender) =>
        {
            var request = new GetDepartment.Query(id);

            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<DepartmentDto>>()
            .WithTags("Department");
    }
}