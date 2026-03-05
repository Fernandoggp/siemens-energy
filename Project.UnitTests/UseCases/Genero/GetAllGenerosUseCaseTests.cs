using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Genero;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;

namespace Project.UnitTests.UseCases.Genero
{
    public class GetAllGenerosUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly GetAllGenerosUseCase _useCase;

        public GetAllGenerosUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<GetAllGenerosUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnGeneros()
        {
            var resultMock = Result.Ok(new List<string> { "Ação", "Drama" });

            _mocker.GetMock<IGeneroService>()
                .Setup(x => x.GetAllGenerosAsync())
                .ReturnsAsync(resultMock);

            var result = await _useCase.ExecuteAsync();

            Assert.True(result.Success);

            _mocker.GetMock<IGeneroService>()
                .Verify(x => x.GetAllGenerosAsync(), Times.Once);
        }
    }
}