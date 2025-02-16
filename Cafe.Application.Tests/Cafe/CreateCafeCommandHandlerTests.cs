using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Application.Actions.Cafe.Create;
using Cafe.Domain.Repositories;
using Moq;

namespace Cafe.Application.Tests.Cafe
{
    public class CreateCafeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WithNewCafeId()
        {
            var cafeRepositoryMock = new Mock<ICafeRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(0))
                .Verifiable();

            var handler = new CreateCafeCommandHandler(
                cafeRepositoryMock.Object,
                unitOfWorkMock.Object);

            var command = new CreateCafeCommand("Cafe Name", "Test description", "Test location");

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess, "Expected result to be success.");
            Assert.NotEqual(Guid.Empty, result.Value);

            cafeRepositoryMock.Verify(r => r.AddCafe(It.IsAny<Domain.Entities.Cafe>()), Times.Once());
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
