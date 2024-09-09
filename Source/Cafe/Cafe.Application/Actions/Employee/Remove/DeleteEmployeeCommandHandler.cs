using Cafe.Domain.Core.Errors;
using Cafe.Domain.Repositories;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Employee.Remove
{
    public class DeleteEmployeeCommandHandler(
       IEmployeeRepository employeeRepository,
       IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteEmployeeCommand, Result<string>>
    {
        public IEmployeeRepository employeeRepository { get; set; } = employeeRepository;
        private IUnitOfWork unitOfWork { get; set; } = unitOfWork;


        public async Task<Result<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetEmployeeByIdAsync(request.id, cancellationToken);
            if (employee == null)
            {
                return Result.Failure<string>(DomainErrors.Employee.NotFound);
            }
            employeeRepository.DeleteEmployee(employee);
            var result = await unitOfWork.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return Result.Failure<string>(DomainErrors.Employee.FailedToUpdate);
            }
            return Result.Success(employee.Id);
        }
    }
}
