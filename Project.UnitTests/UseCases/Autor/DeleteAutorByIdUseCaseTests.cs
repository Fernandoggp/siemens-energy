using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Autor;
using Project.Domain.Interfaces.Services;
using Project.Domain.Common;

namespace Project.UnitTests.UseCases.Autor
{
    public class DeleteAutorByIdUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly DeleteAutorByIdUseCase _useCase;

        public DeleteAutorByIdUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<DeleteAutorByIdUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnError_WhenAutorNotFound()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.GetAutorByIdAsync(id))
                .ReturnsAsync(Result.Fail("Autor não encontrado"));

            var result = await _useCase.ExecuteAsync(id);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnError_WhenAutorHasBooks()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.GetAutorByIdAsync(id))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<ILivroService>()
                .Setup(x => x.GetFilteredLivrosAsync(null, id, null))
                .ReturnsAsync(Result.Ok(new List<object> { new() }));

            var result = await _useCase.ExecuteAsync(id);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldDeleteAutor_WhenNoBooks()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.GetAutorByIdAsync(id))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<ILivroService>()
                .Setup(x => x.GetFilteredLivrosAsync(null, id, null))
                .ReturnsAsync(Result.Ok(new List<object>()));

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.DeleteAutorByIdAsync(id))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(id);

            Assert.True(result.Success);

            _mocker.GetMock<IAutorService>()
                .Verify(x => x.DeleteAutorByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldNotDeleteAutor_WhenAutorHasBooks()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.GetAutorByIdAsync(id))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<ILivroService>()
                .Setup(x => x.GetFilteredLivrosAsync(null, id, null))
                .ReturnsAsync(Result.Ok(new List<object> { new() }));

            var result = await _useCase.ExecuteAsync(id);

            Assert.False(result.Success);

            _mocker.GetMock<IAutorService>()
                .Verify(x => x.DeleteAutorByIdAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}