using Cafe.Application.Actions.Cafe.Get;
using Cafe.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Domain.Entities;

namespace Cafe.Application.Tests.Cafe
{
    public class GetCafeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsSuccess_WithCafeDtos()
        {
            // Arrange
            var fakeCafes = new List<Domain.Entities.Cafe>
            {
                Domain.Entities.Cafe.CreateCafe(
                    name: "Test Cafe",
                    description: "Test Description",
                    location: "TestLocation"
                ),
                Domain.Entities.Cafe.CreateCafe(
                    name: "Test Cafe 2",
                    description: "Test Description 2",
                    location: "TestLocation"
                )
            };

            var cafeRepositoryMock = new Mock<ICafeRepository>();
            cafeRepositoryMock
                .Setup(r => r.GetCafeByLocationAsync("TestLocation", It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeCafes)
                .Verifiable();

            var handler = new GetCafeCommandHandler(cafeRepositoryMock.Object);
            var command = new GetCafeCommand("TestLocation");

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess, "Expected the result to indicate success.");
            Assert.Equal(fakeCafes.Count, result.Value.Count);

            for (int i = 0; i < fakeCafes.Count; i++)
            {
                var expectedCafe = fakeCafes[i];
                var actualCafeDto = result.Value[i];
                Assert.Equal(expectedCafe.Id, actualCafeDto.Id);
                Assert.Equal(expectedCafe.Name, actualCafeDto.Name);
                Assert.Equal(expectedCafe.Description, actualCafeDto.Description);
                Assert.Equal(expectedCafe.Location, actualCafeDto.Location);

                int expectedEmployeeCount = expectedCafe.Employees != null ? expectedCafe.Employees.Count : 0;
                Assert.Equal(expectedEmployeeCount, actualCafeDto.Employees);
            }

            cafeRepositoryMock.Verify(r => r.GetCafeByLocationAsync("TestLocation", It.IsAny<CancellationToken>()),
                Times.Once());
        }
    }
}
