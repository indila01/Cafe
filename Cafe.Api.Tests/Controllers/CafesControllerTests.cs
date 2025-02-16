using Cafe.Api.Controllers;
using Cafe.Api.Requests;
using Cafe.Application.Actions.Cafe;
using Cafe.Application.Actions.Cafe.Create;
using Cafe.Application.Actions.Cafe.Get;
using Cafe.Application.Actions.Cafe.Remove;
using Cafe.Application.Actions.Cafe.Update;
using Cafe.SharedKernel;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Moq;

namespace Cafe.Api.Tests.Controllers
{
    public class CafesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IOptionsSnapshot<ApplicationConfig> _optionsSnapshot;

        public CafesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            var appConfig = new ApplicationConfig { IncludeExceptionDetailsInResponse = false };
            var optionsMock = new Mock<IOptionsSnapshot<ApplicationConfig>>();
            optionsMock.Setup(x => x.Value).Returns(appConfig);
            _optionsSnapshot = optionsMock.Object;
        }

        [Fact]
        public async Task GetCafes_ReturnsOk_WhenResultIsSuccess()
        {
            var expectedValue = new List<CafeDto>();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCafeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(Result.Success(expectedValue)));

            var controller = new CafesController(_mediatorMock.Object, _optionsSnapshot);
            var result = await controller.GetCafes("SomeLocation");

            var okResult = Assert.IsType<Ok<List<CafeDto>>>(result);
            Assert.Equal(expectedValue, okResult.Value);
        }

        [Fact]
        public async Task CreateCafe_ReturnsOk_WhenResultIsSuccess()
        {
            var request = new CafeRequest
            {
                Name = "Sample Cafe",
                Description = "A new cafe",
                Location = "Sample Location"
            };
            var createdId = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateCafeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(createdId));

            var controller = new CafesController(_mediatorMock.Object, _optionsSnapshot);
            var result = await controller.CreateCafe(request);

            var ok = Assert.IsType<Ok<Guid>>(result);
            Assert.Equal(createdId, ok.Value);
        }

        [Fact]
        public async Task UpdateCafe_ReturnsOk_WhenResultIsSuccess()
        {
            var request = new CafeRequest
            {
                Name = "Updated Cafe",
                Description = "Updated Desc",
                Location = "Updated Location"
            };
            var existingId = Guid.NewGuid();
            var expectedValue = new CafeDto(existingId, request.Name, request.Description, request.Location);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateCafeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(expectedValue));

            var controller = new CafesController(_mediatorMock.Object, _optionsSnapshot);
            var result = await controller.UpdateCafe(existingId, request);

            var okResult = Assert.IsType<Ok<CafeDto>>(result);
            Assert.Equal(expectedValue, okResult.Value);
        }

        [Fact]
        public async Task RemoveCafe_ReturnsOk_WhenResultIsSuccess()
        {
            var cafeId = Guid.NewGuid();
            var expectedGuid = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteCafeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(expectedGuid));

            var controller = new CafesController(_mediatorMock.Object, _optionsSnapshot);
            var result = await controller.RemoveCafe(cafeId);

            var okResult = Assert.IsType<Ok<Guid>>(result);
            Assert.Equal(expectedGuid, okResult.Value);
        }
    }
}
