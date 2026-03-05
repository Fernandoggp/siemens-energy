using Moq;
using Moq.AutoMock;
using Project.Application.UseCases.Livro;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;

namespace Project.UnitTests.UseCases.Livro
{
    public class DeleteLivroByIdUseCaseTests
    {
        private readonly AutoMocker _mocker;
        private readonly DeleteLivroByIdUseCase _useCase;

        public DeleteLivroByIdUseCaseTests()
        {
            _mocker = new AutoMocker();
            _useCase = _mocker.CreateInstance<DeleteLivroByIdUseCase>();
        }

        [Fact]
        public async Task ExecuteAsync_ShouldCallService()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<ILivroService>()
                .Setup(x => x.DeleteLivroByIdAsync(id))
                .ReturnsAsync(Result.Ok());

            var result = await _useCase.ExecuteAsync(id);

            Assert.True(result.Success);

            _mocker.GetMock<ILivroService>()
                .Verify(x => x.DeleteLivroByIdAsync(id), Times.Once);
        }
    }
}