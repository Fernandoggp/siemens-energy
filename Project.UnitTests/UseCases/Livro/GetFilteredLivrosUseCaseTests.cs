using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Livro;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;

namespace Project.UnitTests.UseCases.Livro
{
    public class GetFilteredLivrosUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly GetFilteredLivrosUseCase _useCase;

        public GetFilteredLivrosUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<GetFilteredLivrosUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFilteredLivros()
        {
            Guid? livroId = Guid.NewGuid();
            Guid? autorId = Guid.NewGuid();
            Guid? generoId = Guid.NewGuid();

            _mocker.GetMock<ILivroService>()
                .Setup(x => x.GetFilteredLivrosAsync(livroId, autorId, generoId))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(livroId, autorId, generoId);

            Assert.True(result.Success);

            _mocker.GetMock<ILivroService>()
                .Verify(x => x.GetFilteredLivrosAsync(livroId, autorId, generoId), Times.Once);
        }
    }
}