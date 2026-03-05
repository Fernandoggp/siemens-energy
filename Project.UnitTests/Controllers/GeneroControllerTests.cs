using Moq;
using Moq.AutoMock;
using Project.Api.Controllers.V1;
using Project.Api.ViewModel.Dto.Genero;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.UseCases.Genero;

namespace Project.UnitTests.Controllers
{
    public class GeneroControllerTests
    {
        private readonly AutoMocker _mocker;
        private readonly GeneroController _controller;

        public GeneroControllerTests()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<GeneroController>();
        }

        [Fact]
        public async Task ListGeneros_ShouldCallUseCase()
        {
            var result = Result.Ok(new List<string> { "Genero Teste" });

            _mocker.GetMock<IGetAllGenerosUseCase>()
                .Setup(x => x.ExecuteAsync())
                .ReturnsAsync(result);

            var response = await _controller.ListGeneros();

            Assert.NotNull(response);

            _mocker.GetMock<IGetAllGenerosUseCase>()
                .Verify(x => x.ExecuteAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateGenero_ShouldCallUseCase()
        {
            _mocker.GetMock<ICreateGeneroUseCase>()
                .Setup(x => x.ExecuteAsync(It.IsAny<GeneroEntity>()))
                .ReturnsAsync(Result.Ok());

            var dto = new CreateGeneroDto
            {
                Name = "Genero Teste"
            };

            var response = await _controller.CreateGenero(dto);

            Assert.NotNull(response);

            _mocker.GetMock<ICreateGeneroUseCase>()
                .Verify(x => x.ExecuteAsync(It.IsAny<GeneroEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateGenero_ShouldCallUseCase()
        {
            var dto = new UpdateGeneroDto
            {
                Id = Guid.NewGuid(),
                Name = "Genero Atualizado"
            };

            var result = Result.Ok();

            _mocker.GetMock<IUpdateGeneroUseCase>()
                .Setup(x => x.ExecuteAsync(It.IsAny<GeneroEntity>()))
                .ReturnsAsync(result);

            var response = await _controller.UpdateGenero(dto);

            Assert.NotNull(response);

            _mocker.GetMock<IUpdateGeneroUseCase>()
                .Verify(x => x.ExecuteAsync(It.IsAny<GeneroEntity>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGenero_ShouldCallUseCase()
        {
            var id = Guid.NewGuid();

            var result = Result.Ok();

            _mocker.GetMock<IDeleteGeneroByIdUseCase>()
                .Setup(x => x.ExecuteAsync(id))
                .ReturnsAsync(result);

            var response = await _controller.DeleteGenero(id);

            Assert.NotNull(response);

            _mocker.GetMock<IDeleteGeneroByIdUseCase>()
                .Verify(x => x.ExecuteAsync(id), Times.Once);
        }
    }
}