using Moq;
using Moq.AutoMock;
using Project.Application.UseCases;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;
using Project.Domain.Common;

namespace Project.UnitTests.UseCases.Autor
{
    public class CreateAutorUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly CreateAutorUseCase _useCase;

        public CreateAutorUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<CreateAutorUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnValidationError_WhenNameIsInvalid()
        {
            var autor = new AutorEntity { Name = "" };

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.ValidateNameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result.Fail("Nome é obrigatório"));

            var result = await _useCase.ExecuteAsync(autor);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldCreateAutor_WhenNameIsValid()
        {
            var autor = new AutorEntity { Name = "Machado de Assis" };

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.ValidateNameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.CreateAutorAsync(It.IsAny<AutorEntity>()))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(autor);

            Assert.True(result.Success);

            _mocker.GetMock<IAutorService>()
                .Verify(x => x.CreateAutorAsync(It.IsAny<AutorEntity>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldNotCreateAutor_WhenValidationFails()
        {
            var autor = new AutorEntity
            {
                Name = "Nome inválido"
            };

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.ValidateNameAsync(It.IsAny<string>()))
                .ReturnsAsync(Result.Fail("Erro de validação"));

            var result = await _useCase.ExecuteAsync(autor);

            Assert.False(result.Success);

            _mocker.GetMock<IAutorService>()
                .Verify(x => x.CreateAutorAsync(It.IsAny<AutorEntity>()), Times.Never);
        }
    }
}