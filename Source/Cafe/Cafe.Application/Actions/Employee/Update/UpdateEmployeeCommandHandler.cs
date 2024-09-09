using Cafe.Application.Actions.Employee.Get;
using Cafe.Domain.Core.Errors;
using Cafe.Domain.Repositories;
using Cafe.Domain.ValueObjects;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;

namespace Cafe.Application.Actions.Employee.Update
{
    public class UpdateEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        ICafeRepository cafeRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateEmployeeCommand, Result<EmployeeDto>>
    {
        private IEmployeeRepository employeeRepository { get; set; } = employeeRepository;
        private ICafeRepository cafeRepository { get; set; } = cafeRepository;
        private IUnitOfWork unitOfWork { get; set; } = unitOfWork;

        public async Task<Result<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.email);
            var gender = Gender.Create(request.gender);
            var phoneNumber = PhoneNumber.Create(request.phoneNumber.ToString());

            var firstFailureOrSuccess = Result.FirstFailureOrSuccess(email, gender, phoneNumber);
            if (firstFailureOrSuccess.IsFailure)
            {
                return Result.Failure<EmployeeDto>(firstFailureOrSuccess.Error);
            }

            var employee = await employeeRepository.GetEmployeeByIdAsync(request.id, cancellationToken);
            if (employee == null)
            {
                return Result.Failure<EmployeeDto>(DomainErrors.Employee.NotFound);
            }
            
            var cafe = await cafeRepository.GetCafeByIdAsync(request.cafeId, cancellationToken);
            if (cafe == null)
            {
                return Result.Failure<EmployeeDto>(DomainErrors.Cafe.NotFound);
            }

            employee.UpdateEmployee(request.name, email.Value, phoneNumber.Value,gender.Value, cafe.Id);

            employeeRepository.UpdateEmployee(employee);
            var result = await unitOfWork.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return Result.Failure<EmployeeDto>(DomainErrors.Employee.FailedToUpdate);
            }
            return Result.Success(new EmployeeDto(
                employee.Id, 
                employee.Name, 
                employee.Email, 
                employee.PhoneNumber, 
                DaysWorked: employee.StartDate != null ? (int)(DateTime.Now - employee.StartDate).Value.TotalDays : 0,
                cafe.Name,
                cafe.Id));
        }
    }
}
