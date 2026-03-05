using Moq;
using Moq.AutoMock;
using Project.Application.UseCases;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;

namespace Project.UnitTests.UseCases.Livro
{
    public class CreateLivroUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly CreateLivroUseCase _useCase;

        public CreateLivroUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<CreateLivroUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldCreateLivro_WhenValid()
        {
            var livro = new LivroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Livro Teste",
                AutorId = Guid.NewGuid(),
                GeneroId = Guid.NewGuid()
            };

            _mocker.GetMock<ILivroService>()
                .Setup(x => x.ValidateNameAsync(livro.Name))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<IAutorService>()
                .Setup(x => x.GetAutorByIdAsync(livro.AutorId))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.GetGeneroByIdAsync(livro.GeneroId))
                .ReturnsAsync(Result.Ok());

            _mocker.GetMock<ILivroService>()
                .Setup(x => x.CreateLivroAsync(livro))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(livro);

            Assert.True(result.Success);

            _mocker.GetMock<ILivroService>()
                .Verify(x => x.CreateLivroAsync(livro), Times.Once);
        }
    }
}