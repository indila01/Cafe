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
        : IRequestHandler<GetEmployeeCommand, Result<List<EmployeeDto>>>
    {
        public IEmployeeRepository employeeRepository { get; set; } = employeeRepository;
        public async Task<Result<List<EmployeeDto>>> Handle(GetEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await employeeRepository.GetEmployeebyCafeAsync(request.Cafe, cancellationToken);
            if (result != null)
            {
                var empList = result.Select(e => new EmployeeDto(
                     e.Id,
                     e.Name,
                     e.Email,
                     e.PhoneNumber,
                     DaysWorked: e.StartDate != null ? (int)(DateTime.Now - e.StartDate).Value.TotalDays : 0,
                     e.Cafe?.Name ?? string.Empty))
                     .OrderByDescending(x => x.DaysWorked)
                     .ToList();
    
                return Result.Success(empList);
            }
            return Result.Success(new List<EmployeeDto>());
        }
    }
}
