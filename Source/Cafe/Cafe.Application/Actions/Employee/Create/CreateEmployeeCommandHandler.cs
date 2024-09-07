using Cafe.Domain.Core.Errors;
using Cafe.Domain.Repositories;
using Cafe.Domain.ValueObjects;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Employee.Create
{
    public class CreateEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        ICafeRepository cafeRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CreateEmployeeCommand, Result<string>>
    {
        private IEmployeeRepository employeeRepository { get; set; } = employeeRepository;
        private ICafeRepository cafeRepository { get; set; } = cafeRepository;
        private IUnitOfWork unitOfWork { get; set; } = unitOfWork;

        public async Task<Result<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.email);
            var gender = Gender.Create(request.gender);
            var phoneNumber = PhoneNumber.Create(request.phoneNumber);

            var firstFailureOrSuccess = Result.FirstFailureOrSuccess(email, gender, phoneNumber);
            if (firstFailureOrSuccess.IsFailure)
            {
                return Result.Failure<string>(firstFailureOrSuccess.Error);
            }

            var uniqueId = await GenerateUniqueIdAsync();

            var cafe = !request.cafeId.Equals(Guid.Empty) ?
                await cafeRepository.GetCafeByIdAsync(request.cafeId) : null;

            if (!request.cafeId.Equals(Guid.Empty) && cafe == null)
            {
                return Result.Failure<string>(DomainErrors.Cafe.NotFound);
            }

            var employee = Domain.Entities.Employee.CreateEmployee(
                uniqueId, request.name, email.Value, phoneNumber.Value, gender.Value, cafe);

            employeeRepository.AddEmployee(employee);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(employee.Id);
        }

        private async Task<string> GenerateUniqueIdAsync()
        {
            const string prefix = "UI";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            string uniqueId;

            do
            {
                uniqueId = prefix + new string(Enumerable.Repeat(chars, 7)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            } while (!await employeeRepository.CheckUniqueId(uniqueId));

            return uniqueId;
        }
    }
}
