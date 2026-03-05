using Moq;
using Moq.AutoMock;
using Project.Api.Controllers.V1;
using Project.Domain.Interfaces.UseCases.Autor;
using Project.Domain.Common;
using Project.Api.ViewModel.Dto.Autor;
using Project.Domain.Entities;

namespace Project.UnitTests.Controllers
{
    public class AutorControllerTests
    {
        private readonly AutoMocker _mocker;
        private readonly AutorController _controller;

        public AutorControllerTests()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<AutorController>();
        }

        [Fact]
        public async Task ListAutores_ShouldCallUseCase()
        {
            var result = Result.Ok(new List<string> { "Autor Teste" });

            _mocker.GetMock<IGetAllAutoresUseCase>()
                .Setup(x => x.ExecuteAsync())
                .ReturnsAsync(result);

            var response = await _controller.ListAutores();

            Assert.NotNull(response);

            _mocker.GetMock<IGetAllAutoresUseCase>()
                .Verify(x => x.ExecuteAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAutor_ShouldCallUseCase()
        {
            _mocker.GetMock<ICreateAutorUseCase>()
                .Setup(x => x.ExecuteAsync(It.IsAny<AutorEntity>()))
                .ReturnsAsync(Result.Ok());

            var dto = new CreateAutorDto
            {
                Name = "Autor Teste"
            };

            var response = await _controller.CreateAutor(dto);

            Assert.NotNull(response);

            _mocker.GetMock<ICreateAutorUseCase>()
                .Verify(x => x.ExecuteAsync(It.IsAny<AutorEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAutor_ShouldCallUseCase()
        {
            var dto = new UpdateAutorDto
            {
                Id = Guid.NewGuid(),
                Name = "Autor Atualizado"
            };

            var result = Result.Ok();

            _mocker.GetMock<IUpdateAutorUseCase>()
                .Setup(x => x.ExecuteAsync(It.IsAny<AutorEntity>()))
                .ReturnsAsync(result);

            var response = await _controller.UpdateAutor(dto);

            Assert.NotNull(response);

            _mocker.GetMock<IUpdateAutorUseCase>()
                .Verify(x => x.ExecuteAsync(It.IsAny<AutorEntity>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAutor_ShouldCallUseCase()
        {
            var id = Guid.NewGuid();

            var result = Result.Ok();

            _mocker.GetMock<IDeleteAutorByIdUseCase>()
                .Setup(x => x.ExecuteAsync(id))
                .ReturnsAsync(result);

            var response = await _controller.DeleteAutor(id);

            Assert.NotNull(response);

            _mocker.GetMock<IDeleteAutorByIdUseCase>()
                .Verify(x => x.ExecuteAsync(id), Times.Once);
        }
    }
}