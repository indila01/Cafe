using Cafe.Domain.Repositories;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Employee.Get
{
    public class GetEmployeeCommandHandler(IEmployeeRepository employeeRepository) 
        : IRequestHandler<EmployeeCommand, Result<List<EmployeeDto>>>
    {
        public IEmployeeRepository employeeRepository { get; set; } = employeeRepository;
        public async Task<Result<List<EmployeeDto>>> Handle(EmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await employeeRepository.GetEmployeebyCafeAsync(request.Cafe, cancellationToken);
            return Result.Success(new List<EmployeeDto>());
        }
    }
}
