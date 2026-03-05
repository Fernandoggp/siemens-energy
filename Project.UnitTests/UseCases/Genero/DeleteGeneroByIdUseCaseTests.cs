using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Genero;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;

namespace Project.UnitTests.UseCases.Genero
{
    public class DeleteGeneroByIdUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly DeleteGeneroByIdUseCase _useCase;

        public DeleteGeneroByIdUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<DeleteGeneroByIdUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldFail_WhenGeneroNotFound()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.GetGeneroByIdAsync(id))
                .ReturnsAsync(Result.Fail("Genero não encontrado"));

            var result = await _useCase.ExecuteAsync(id);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldFail_WhenGeneroHasBooks()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.GetGeneroByIdAsync(id))
                .ReturnsAsync(Result.Ok(new object()));

            _mocker.GetMock<ILivroService>()
                .Setup(x => x.GetFilteredLivrosAsync(null, null, id))
                .ReturnsAsync(Result.Ok(new List<object> { new object() }));

            var result = await _useCase.ExecuteAsync(id);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldDeleteGenero_WhenNoBooks()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.GetGeneroByIdAsync(id))
                .ReturnsAsync(Result.Ok(new object()));

            _mocker.GetMock<ILivroService>()
                .Setup(x => x.GetFilteredLivrosAsync(null, null, id))
                .ReturnsAsync(Result.Ok(new List<object>()));

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.DeleteGeneroByIdAsync(id))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(id);

            Assert.True(result.Success);

            _mocker.GetMock<IGeneroService>()
                .Verify(x => x.DeleteGeneroByIdAsync(id), Times.Once);
        }
    }
}