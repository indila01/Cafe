using Cafe.Application.Actions.Employee.Get;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;

namespace Cafe.Application.Actions.Employee.Update
{
    public record UpdateEmployeeCommand(
        string id,
        string name,
        string gender,
        string email,
        string phoneNumber,
        Guid cafeId
        ) : IRequest<Result<EmployeeDto>>;
}