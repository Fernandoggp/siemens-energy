using Moq;
using Moq.AutoMock;
using Project.Application.Services;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;

namespace Project.UnitTests.Services
{
    public class GeneroServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly GeneroService _generoService;

        public GeneroServiceTests()
        {
            _mocker = new AutoMocker();
            _generoService = _mocker.CreateInstance<GeneroService>();
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameIsEmpty()
        {
            var result = await _generoService.ValidateNameAsync("");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameHasInvalidCharacters()
        {
            var result = await _generoService.ValidateNameAsync("Genero123");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameLengthInvalid()
        {
            var result = await _generoService.ValidateNameAsync("AB");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenGeneroAlreadyExists()
        {
            var name = "Drama";

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.ExistsByNameAsync(name))
                .ReturnsAsync(true);

            var result = await _generoService.ValidateNameAsync(name);

            Assert.False(result.Success);

            _mocker.GetMock<IGeneroRepository>()
                .Verify(x => x.ExistsByNameAsync(name), Times.Once);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldPass_WhenNameIsValid()
        {
            var name = "Drama";

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.ExistsByNameAsync(name))
                .ReturnsAsync(false);

            var result = await _generoService.ValidateNameAsync(name);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateGeneroAsync_ShouldFail_WhenGeneroIsNull()
        {
            var result = await _generoService.CreateGeneroAsync(null);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task CreateGeneroAsync_ShouldCreateGenero_WhenValid()
        {
            var genero = new GeneroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Ação"
            };

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.CreateAsync(genero))
                .ReturnsAsync(genero);

            var result = await _generoService.CreateGeneroAsync(genero);

            Assert.True(result.Success);

            _mocker.GetMock<IGeneroRepository>()
                .Verify(x => x.CreateAsync(genero), Times.Once);
        }

        [Fact]
        public async Task GetAllGenerosAsync_ShouldReturnGeneros()
        {
            var generos = new List<GeneroEntity>
            {
                new GeneroEntity { Id = Guid.NewGuid(), Name = "Ação" }
            };

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.GetAllGenerosAsync())
                .ReturnsAsync(generos);

            var result = await _generoService.GetAllGenerosAsync();

            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateGeneroAsync_ShouldFail_WhenGeneroNotFound()
        {
            var genero = new GeneroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Drama"
            };

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.UpdateAsync(genero))
                .ReturnsAsync((GeneroEntity)null);

            var result = await _generoService.UpdateGeneroAsync(genero);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateGeneroAsync_ShouldUpdateGenero_WhenFound()
        {
            var genero = new GeneroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Drama"
            };

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.UpdateAsync(genero))
                .ReturnsAsync(genero);

            var result = await _generoService.UpdateGeneroAsync(genero);

            Assert.True(result.Success);

            _mocker.GetMock<IGeneroRepository>()
                .Verify(x => x.UpdateAsync(genero), Times.Once);
        }

        [Fact]
        public async Task DeleteGeneroByIdAsync_ShouldFail_WhenGeneroNotFound()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.DeleteGeneroByIdAsync(id))
                .ReturnsAsync(false);

            var result = await _generoService.DeleteGeneroByIdAsync(id);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteGeneroByIdAsync_ShouldDeleteGenero_WhenFound()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.DeleteGeneroByIdAsync(id))
                .ReturnsAsync(true);

            var result = await _generoService.DeleteGeneroByIdAsync(id);

            Assert.True(result.Success);

            _mocker.GetMock<IGeneroRepository>()
                .Verify(x => x.DeleteGeneroByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetGeneroByIdAsync_ShouldFail_WhenGeneroNotFound()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.GetGeneroByIdAsync(id))
                .ReturnsAsync((GeneroEntity)null);

            var result = await _generoService.GetGeneroByIdAsync(id);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task GetGeneroByIdAsync_ShouldReturnGenero_WhenFound()
        {
            var id = Guid.NewGuid();

            var genero = new GeneroEntity
            {
                Id = id,
                Name = "Ação"
            };

            _mocker.GetMock<IGeneroRepository>()
                .Setup(x => x.GetGeneroByIdAsync(id))
                .ReturnsAsync(genero);

            var result = await _generoService.GetGeneroByIdAsync(id);

            Assert.True(result.Success);
        }
    }
}