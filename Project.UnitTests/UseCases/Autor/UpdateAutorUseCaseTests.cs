using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Autor;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;
using Project.Domain.Common;

namespace Project.UnitTests.UseCases.Autor
{
    public class UpdateAutorUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly UpdateAutorUseCase _useCase;

        public UpdateAutorUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<UpdateAutorUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnError_WhenValidationFails()
        {
            var autor = new AutorEntity { Name = "" };

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.ValidateNameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result.Fail("Erro"));

            var result = await _useCase.ExecuteAsync(autor);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldUpdateAutor_WhenValidationPasses()
        {
            var autor = new AutorEntity { Name = "Autor Atualizado" };

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.ValidateNameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.UpdateAutorAsync(It.IsAny<AutorEntity>()))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(autor);

            Assert.True(result.Success);

            _mocker.GetMock<IAutorService>()
                .Verify(x => x.UpdateAutorAsync(It.IsAny<AutorEntity>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldNotUpdateAutor_WhenValidationFails()
        {
            var autor = new AutorEntity
            {
                Id = Guid.NewGuid(),
                Name = ""
            };

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.ValidateNameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result.Fail("Nome inválido"));

            var result = await _useCase.ExecuteAsync(autor);

            Assert.False(result.Success);

            _mocker.GetMock<IAutorService>()
                .Verify(x => x.UpdateAutorAsync(It.IsAny<AutorEntity>()), Times.Never);
        }
    }
}