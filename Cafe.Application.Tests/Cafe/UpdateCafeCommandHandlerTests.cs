using Cafe.Application.Actions.Cafe.Update;
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
    public class UpdateCafeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsSuccess_WhenCafeIsUpdated()
        {
            // Arrange
            var fakeCafe = Domain.Entities.Cafe.CreateCafe(
                name: "Original Name",
                description: "Original Description",
                location: "Original Location"
            );
            // Setup GetCafeByIdAsync to return the fake café.
            var cafeRepositoryMock = new Mock<ICafeRepository>();
            cafeRepositoryMock
                .Setup(r => r.GetCafeByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeCafe);

            // Setup UpdateCafe to do nothing (void).
            cafeRepositoryMock.Setup(r => r.UpdateCafe(fakeCafe));

            // Setup unit of work to return 1 (indicating one change persisted).
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var handler = new UpdateCafeCommandHandler(cafeRepositoryMock.Object, unitOfWorkMock.Object);
            var command = new UpdateCafeCommand(
                id: fakeCafe.Id,
                name: "New Name",
                description: "New Description",
                location: "New Location"
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess, "Expected the update result to be success.");
            Assert.Equal(fakeCafe.Id, result.Value.Id);
            Assert.Equal("New Name", result.Value.Name);
            Assert.Equal("New Description", result.Value.Description);
            Assert.Equal("New Location", result.Value.Location);

            // Verify that the repository methods and unit of work were called once.
            cafeRepositoryMock.Verify(r => r.GetCafeByIdAsync(fakeCafe.Id, It.IsAny<CancellationToken>()), Times.Once());
            cafeRepositoryMock.Verify(r => r.UpdateCafe(fakeCafe), Times.Once());
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenCafeNotFound()
        {
            // Arrange: GetCafeByIdAsync returns null.
            var cafeRepositoryMock = new Mock<ICafeRepository>();
            cafeRepositoryMock
                .Setup(r => r.GetCafeByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Cafe)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var handler = new UpdateCafeCommandHandler(cafeRepositoryMock.Object, unitOfWorkMock.Object);
            var command = new UpdateCafeCommand(
                id: Guid.NewGuid(),
                name: "New Name",
                description: "New Description",
                location: "New Location"
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess, "Expected the update result to be failure.");
            Assert.Equal(DomainErrors.Cafe.NotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenSaveReturnsZero()
        {
            // Arrange
            var fakeCafe =  Domain.Entities.Cafe.CreateCafe(
                name: "Original Name",
                description: "Original Description",
                location: "Original Location"
            );

            var cafeRepositoryMock = new Mock<ICafeRepository>();
            cafeRepositoryMock
                .Setup(r => r.GetCafeByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeCafe);
            cafeRepositoryMock.Setup(r => r.UpdateCafe(fakeCafe));

            // Simulate SaveChangesAsync returning 0.
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            var handler = new UpdateCafeCommandHandler(cafeRepositoryMock.Object, unitOfWorkMock.Object);
            var command = new UpdateCafeCommand(
                id: fakeCafe.Id,
                name: "New Name",
                description: "New Description",
                location: "New Location"
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess, "Expected the update result to be failure.");
            Assert.Equal(DomainErrors.Cafe.FailedToUpdate, result.Error);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var cafeRepositoryMock = new Mock<ICafeRepository>();
            cafeRepositoryMock
                .Setup(r => r.GetCafeByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var handler = new UpdateCafeCommandHandler(cafeRepositoryMock.Object, unitOfWorkMock.Object);
            var command = new UpdateCafeCommand(
                id: Guid.NewGuid(),
                name: "New Name",
                description: "New Description",
                location: "New Location"
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess, "Expected the update result to be failure.");
            Assert.Equal(DomainErrors.Cafe.FailedToUpdate, result.Error);
        }
    }
}
