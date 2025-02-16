using Cafe.Application.Actions.Cafe.Remove;
using Cafe.Domain.Core.Errors;
using Cafe.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Tests.Cafe
{
    public class DeleteCafeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsSuccess_WhenCafeIsDeleted()
        {
            // Arrange
            var fakeCafe = Domain.Entities.Cafe.CreateCafe(
                name: "Test Cafe",
                description: "Test Description",
                location: "Test Location"
            );

            var cafeRepositoryMock = new Mock<ICafeRepository>();
            cafeRepositoryMock
                .Setup(r => r.GetCafeByIdAsync(fakeCafe.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeCafe);
            cafeRepositoryMock.Setup(r => r.DeleteCafe(fakeCafe));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var handler = new DeleteCafeCommandHandler(cafeRepositoryMock.Object, unitOfWorkMock.Object);
            var command = new DeleteCafeCommand(fakeCafe.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess, "Expected the delete operation to succeed.");
            Assert.Equal(fakeCafe.Id, result.Value);

            cafeRepositoryMock.Verify(r => r.GetCafeByIdAsync(fakeCafe.Id, It.IsAny<CancellationToken>()), Times.Once());
            cafeRepositoryMock.Verify(r => r.DeleteCafe(fakeCafe), Times.Once());
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenCafeNotFound()
        {
            // Arrange
            var cafeRepositoryMock = new Mock<ICafeRepository>();
            cafeRepositoryMock
                .Setup(r => r.GetCafeByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Cafe)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var handler = new DeleteCafeCommandHandler(cafeRepositoryMock.Object, unitOfWorkMock.Object);
            var command = new DeleteCafeCommand(Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess, "Expected the delete operation to fail when cafe is not found.");
            Assert.Equal(DomainErrors.Cafe.NotFound, result.Error);

            cafeRepositoryMock.Verify(r => r.GetCafeByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenSaveChangesFails()
        {
            // Arrange
            var fakeCafe = Domain.Entities.Cafe.CreateCafe(
                name: "Test Cafe",
                description: "Test Description",
                location: "Test Location"
            );

            var cafeRepositoryMock = new Mock<ICafeRepository>();
            cafeRepositoryMock
                .Setup(r => r.GetCafeByIdAsync(fakeCafe.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeCafe);
            cafeRepositoryMock.Setup(r => r.DeleteCafe(fakeCafe));

            // Simulate SaveChangesAsync returning 0.
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            var handler = new DeleteCafeCommandHandler(cafeRepositoryMock.Object, unitOfWorkMock.Object);
            var command = new DeleteCafeCommand(fakeCafe.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess, "Expected the delete operation to fail when changes are not persisted.");
            Assert.Equal(DomainErrors.Cafe.FailedToUpdate, result.Error);

            cafeRepositoryMock.Verify(r => r.GetCafeByIdAsync(fakeCafe.Id, It.IsAny<CancellationToken>()), Times.Once());
            cafeRepositoryMock.Verify(r => r.DeleteCafe(fakeCafe), Times.Once());
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
