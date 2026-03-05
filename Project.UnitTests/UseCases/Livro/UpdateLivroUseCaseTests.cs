using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Livro;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;

namespace Project.UnitTests.UseCases.Livro
{
    public class UpdateLivroUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly UpdateLivroUseCase _useCase;

        public UpdateLivroUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<UpdateLivroUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldUpdateLivro_WhenValid()
        {
            var livro = new LivroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Livro Atualizado",
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
                .Setup(x => x.UpdateLivroAsync(livro))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(livro);

            Assert.True(result.Success);

            _mocker.GetMock<ILivroService>()
                .Verify(x => x.UpdateLivroAsync(livro), Times.Once);
        }
    }
}