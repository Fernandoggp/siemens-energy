using Moq;
using Moq.AutoMock;
using Project.Application.UseCases;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;

namespace Project.UnitTests.UseCases.Genero
{
    public class CreateGeneroUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly CreateGeneroUseCase _useCase;

        public CreateGeneroUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<CreateGeneroUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldCreateGenero_WhenNameIsValid()
        {
            var genero = new GeneroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Ação"
            };

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.ValidateNameAsync(genero.Name))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.CreateGeneroAsync(genero))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(genero);

            Assert.True(result.Success);

            _mocker.GetMock<IGeneroService>()
                .Verify(x => x.CreateGeneroAsync(genero), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFail_WhenNameIsInvalid()
        {
            var genero = new GeneroEntity
            {
                Name = "Ação"
            };

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.ValidateNameAsync(genero.Name))
                .ReturnsAsync(Result.Fail("Nome inválido"));

            var result = await _useCase.ExecuteAsync(genero);

            Assert.False(result.Success);

            _mocker.GetMock<IGeneroService>()
                .Verify(x => x.CreateGeneroAsync(It.IsAny<GeneroEntity>()), Times.Never);
        }
    }
}