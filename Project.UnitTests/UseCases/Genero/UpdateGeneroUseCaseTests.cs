using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Genero;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;

namespace Project.UnitTests.UseCases.Genero
{
    public class UpdateGeneroUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly UpdateGeneroUseCase _useCase;

        public UpdateGeneroUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<UpdateGeneroUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldUpdateGenero_WhenValid()
        {
            var genero = new GeneroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Drama"
            };

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.ValidateNameAsync(genero.Name))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.UpdateGeneroAsync(genero))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(genero);

            Assert.True(result.Success);

            _mocker.GetMock<IGeneroService>()
                .Verify(x => x.UpdateGeneroAsync(genero), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldFail_WhenNameInvalid()
        {
            var genero = new GeneroEntity
            {
                Name = "Drama"
            };

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.ValidateNameAsync(genero.Name))
                .ReturnsAsync(Result.Fail("Nome inválido"));

            var result = await _useCase.ExecuteAsync(genero);

            Assert.False(result.Success);

            _mocker.GetMock<IGeneroService>()
                .Verify(x => x.UpdateGeneroAsync(It.IsAny<GeneroEntity>()), Times.Never);
        }
    }
}