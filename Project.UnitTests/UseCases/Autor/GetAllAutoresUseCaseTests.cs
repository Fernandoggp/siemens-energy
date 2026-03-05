using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Autor;
using Project.Domain.Interfaces.Services;
using Project.Domain.Common;

namespace Project.UnitTests.UseCases.Autor
{
    public class GetAllAutoresUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly GetAllAutoresUseCase _useCase;

        public GetAllAutoresUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<GetAllAutoresUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnAutores()
        {
            _mocker.GetMock<IAutorService>()
                .Setup(x => x.GetAllAutoresAsync())
                .ReturnsAsync(Result.Ok(new List<string>()));

            var result = await _useCase.ExecuteAsync();

            Assert.True(result.Success);

            _mocker.GetMock<IAutorService>()
                .Verify(x => x.GetAllAutoresAsync(), Times.Once);
        }
    }
}