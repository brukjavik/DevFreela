using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Fakes;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class InsertProjectHandlerTests
    {
        [Fact]
        public async Task InputDataAreOk_Insert_Success_NSubstitute()
        {
            // Arrange
            const int ID = 1;
            var repository = Substitute.For<IProjectRepository>();
            repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(ID));

            /*var command = new InsertProjectCommand
            {
                Title = "Nome do Projeto",
                Description = "Descrição do Projeto",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 5000
            };*/

            var command = FakeDataHelper.CreateFakeInsertProjectCommand();

            var handler = new InsertProjectHandler(repository);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ID, result.Data);
            await repository.Received(1).Add(Arg.Any<Project>());
        }
        [Fact]
        public async Task InputDataAreOk_Insert_Success_Moq()
        {
            // Arrange
            const int ID = 1;

            var repository = Mock.Of<IProjectRepository>(r => r.Add(It.IsAny<Project>()) == Task.FromResult(ID));

            /*var command = new InsertProjectCommand
            {
                Title = "Nome do Projeto",
                Description = "Descrição do Projeto",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 5000
            };*/

            var command = FakeDataHelper.CreateFakeInsertProjectCommand();

            var handler = new InsertProjectHandler(repository);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert

            //Assert.True(result.IsSuccess);
            //Assert.Equal(ID, result.Data);

            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(ID);

            Mock.Get(repository).Verify(r => r.Add(It.IsAny<Project>()), Times.Once);
        }
    }
}
