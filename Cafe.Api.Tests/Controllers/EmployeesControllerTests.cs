using Cafe.Api.Controllers;
using Cafe.Api.Requests;
using Cafe.Application.Actions.Employee;
using Cafe.Application.Actions.Employee.Create;
using Cafe.Application.Actions.Employee.Get;
using Cafe.Application.Actions.Employee.Remove;
using Cafe.Application.Actions.Employee.Update;
using Cafe.SharedKernel;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Moq;

namespace Cafe.Api.Tests.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IOptionsSnapshot<ApplicationConfig> _optionsSnapshot;

        public EmployeesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();

            var appConfig = new ApplicationConfig { IncludeExceptionDetailsInResponse = false };
            var optionsMock = new Mock<IOptionsSnapshot<ApplicationConfig>>();
            optionsMock.Setup(x => x.Value).Returns(appConfig);
            _optionsSnapshot = optionsMock.Object;
        }

        [Fact]
        public async Task GetEmployees_ReturnsOk_WhenResultIsSuccess()
        {
            // Arrange
            var expectedValue = new List<EmployeeDto>();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetEmployeeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Success(expectedValue)))
                .Verifiable();

            var controller = new EmployeesController(_mediatorMock.Object, _optionsSnapshot);

            var result = await controller.GetEmployees("SomeCafe");

            var okResult = Assert.IsType<Ok<List<EmployeeDto>>>(result);
            Assert.Equal(expectedValue, okResult.Value);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsOk_WhenResultIsSuccess()
        {
            // Arrange
            var request = new EmployeeRequest
            {
                Name = "John Doe",
                Gender = "Male",
                Email = "john@example.com",
                PhoneNumber = 1234567890,
                CafeId = Guid.NewGuid()
            };

            var expectedValue = Guid.NewGuid().ToString();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Success(expectedValue)));

            var controller = new EmployeesController(_mediatorMock.Object, _optionsSnapshot);

            var result = await controller.CreateEmployee(request);

            var ok = Assert.IsType<Ok<string>>(result);
            Assert.Equal(expectedValue, ok.Value);
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsOk_WhenResultIsSuccess()
        {
            var request = new EmployeeRequest
            {
                Name = "Jane Doe",
                Gender = "Female",
                Email = "jane@example.com",
                PhoneNumber = 9876543210,
                CafeId = Guid.NewGuid()
            };

            var employeeId = "employee123";
            var expectedValue = new EmployeeDto
            (
                employeeId,
                request.Name,
                request.Email,
                request.PhoneNumber,
                2,
                null,
                request.CafeId
            );

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateEmployeeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Success(expectedValue)));

            var controller = new EmployeesController(_mediatorMock.Object, _optionsSnapshot);

            var result = await controller.UpdateEmployee(employeeId, request);

            var okResult = Assert.IsType<Ok<EmployeeDto>>(result);
            Assert.Equal(expectedValue, okResult.Value);
        }

        [Fact]
        public async Task RemoveEmployee_ReturnsOk_WhenResultIsSuccess()
        {
            var employeeId = "employee123";

            var expectedValue = "Employee removed successfully";

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteEmployeeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result.Success(expectedValue)));

            var controller = new EmployeesController(_mediatorMock.Object, _optionsSnapshot);

            var result = await controller.RemoveEmployee(employeeId);

            var okResult = Assert.IsType<Ok<string>>(result);
            Assert.Equal(expectedValue, okResult.Value);
        }
    }
}
