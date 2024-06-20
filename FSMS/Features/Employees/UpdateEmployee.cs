using Carter;
using FSMS.Contracts.Request;
using FSMS.Data;
using FSMS.Extensions;
using FSMS.Features.Employees;
using MediatR;

namespace FSMS.Features.Employees
{
    public class UpdateEmployee
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public Guid DepartmentId { get; set; }
        }

        public sealed class Handler(AppDbContext context) : IRequestHandler<Command, Result<Guid>>
        {
            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var employee = await context.Employees.FindAsync(request.Id);

                if (employee is null)
                {
                    return Result.Failure<Guid>(Error.NonExistentEmployee);
                }

                employee.FirstName = request.FirstName;
                employee.LastName = request.LastName;
                employee.Email = request.Email;
                employee.PhoneNumber = request.PhoneNumber;
                employee.DepartmentId = request.DepartmentId;

                await context.SaveChangesAsync(cancellationToken);

                return employee.Id;
            }
        }
    }
}

public class UpdateEmployeeEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/employee/{id:guid}", async (Guid id, CreateEmployeeDto employee, ISender sender) =>
        {
            var request = new UpdateEmployee.Command
            {
                Id = id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DepartmentId = employee.DepartmentId
            };

            var result = await sender.Send(request);

            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        })
            .Produces<Result<Guid>>()
            .WithTags("Employee");
    }
}
