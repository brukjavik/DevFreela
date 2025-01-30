using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.UnitTests.Fakes;
using FluentAssertions;

namespace DevFreela.UnitTests.Core
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectIsCreated_Start_Success() /* Naming Convention: Given_When_Then */
        {
            // Arrange
            //var project = new Project("Nome do Projeto", "Descrição do Projeto", 1, 2, 5000);
            var project = FakeDataHelper.CreateFakeProject();
            // Act
            project.Start();
            // Assert

            //Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            //Assert.NotNull(project.StartedAt);

            project.Status.Should().Be(ProjectStatusEnum.InProgress);
            project.StartedAt.Should().NotBeNull();
        }

        [Fact]
        public void ProjectIsInInvalidState_Start_ThrowsException()
        {
            // Arrange
            //var project = new Project("Nome do Projeto", "Descrição do Projeto", 1, 2, 5000);
            var project = FakeDataHelper.CreateFakeProject();
            project.Start();
            // Act + Assert

            Action? start = project.Start;

            //var exception = Assert.Throws<InvalidOperationException>(start);
            //Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);

            start.Should().Throw<InvalidOperationException>().WithMessage(Project.INVALID_STATE_MESSAGE);
        }
    }
}
