using Moq;
using Moq.AutoMock;
using Project.Api.Controllers.V1;
using Project.Api.ViewModel.Dto.Livro;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.UseCases.Livro;

namespace Project.UnitTests.Controllers
{
    public class LivroControllerTests
    {
        private readonly AutoMocker _mocker;
        private readonly LivroController _controller;

        public LivroControllerTests()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<LivroController>();
        }

        [Fact]
        public async Task ListLivros_ShouldCallUseCase()
        {
            var result = Result.Ok(new List<string> { "Livro Teste" });

            _mocker.GetMock<IGetAllLivrosUseCase>()
                .Setup(x => x.ExecuteAsync())
                .ReturnsAsync(result);

            var response = await _controller.ListLivros();

            Assert.NotNull(response);

            _mocker.GetMock<IGetAllLivrosUseCase>()
                .Verify(x => x.ExecuteAsync(), Times.Once);
        }

        [Fact]
        public async Task FilteredLivros_ShouldCallUseCase()
        {
            var livroId = Guid.NewGuid();
            var autorId = Guid.NewGuid();
            var generoId = Guid.NewGuid();

            var result = Result.Ok(new List<string> { "Livro Filtrado" });

            _mocker.GetMock<IGetFilteredLivrosUseCase>()
                .Setup(x => x.ExecuteAsync(livroId, autorId, generoId))
                .ReturnsAsync(result);

            var response = await _controller.FilteredLivros(livroId, autorId, generoId);

            Assert.NotNull(response);

            _mocker.GetMock<IGetFilteredLivrosUseCase>()
                .Verify(x => x.ExecuteAsync(livroId, autorId, generoId), Times.Once);
        }

        [Fact]
        public async Task CreateLivro_ShouldCallUseCase()
        {
            _mocker.GetMock<ICreateLivroUseCase>()
                .Setup(x => x.ExecuteAsync(It.IsAny<LivroEntity>()))
                .ReturnsAsync(Result.Ok());

            var dto = new CreateLivroDto
            {
                Name = "Livro Teste",
                AutorId = Guid.NewGuid(),
                GeneroId = Guid.NewGuid()
            };

            var response = await _controller.CreateLivro(dto);

            Assert.NotNull(response);

            _mocker.GetMock<ICreateLivroUseCase>()
                .Verify(x => x.ExecuteAsync(It.IsAny<LivroEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateLivro_ShouldCallUseCase()
        {
            var dto = new UpdateLivroDto
            {
                Id = Guid.NewGuid(),
                Name = "Livro Atualizado",
                AutorId = Guid.NewGuid(),
                GeneroId = Guid.NewGuid()
            };

            var result = Result.Ok();

            _mocker.GetMock<IUpdateLivroUseCase>()
                .Setup(x => x.ExecuteAsync(It.IsAny<LivroEntity>()))
                .ReturnsAsync(result);

            var response = await _controller.UpdateLivro(dto);

            Assert.NotNull(response);

            _mocker.GetMock<IUpdateLivroUseCase>()
                .Verify(x => x.ExecuteAsync(It.IsAny<LivroEntity>()), Times.Once);
        }

        [Fact]
        public async Task DeleteLivro_ShouldCallUseCase()
        {
            var id = Guid.NewGuid();

            var result = Result.Ok();

            _mocker.GetMock<IDeleteLivroByIdUseCase>()
                .Setup(x => x.ExecuteAsync(id))
                .ReturnsAsync(result);

            var response = await _controller.DeleteLivro(id);

            Assert.NotNull(response);

            _mocker.GetMock<IDeleteLivroByIdUseCase>()
                .Verify(x => x.ExecuteAsync(id), Times.Once);
        }
    }
}