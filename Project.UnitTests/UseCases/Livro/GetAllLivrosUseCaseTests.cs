using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Livro;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;

namespace Project.UnitTests.UseCases.Livro
{
    public class GetAllLivrosUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly GetAllLivrosUseCase _useCase;

        public GetAllLivrosUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<GetAllLivrosUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnLivros()
        {
            _mocker.GetMock<ILivroService>()
                .Setup(x => x.GetAllLivrosAsync())
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync();

            Assert.True(result.Success);

            _mocker.GetMock<ILivroService>()
                .Verify(x => x.GetAllLivrosAsync(), Times.Once);
        }
    }
}