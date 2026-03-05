using Moq;
using Project.Application.Services;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Xunit;

namespace Project.UnitTests.Services
{
    public class AutorServiceTests
    {
        private readonly Mock<IAutorRepository> _autorRepositoryMock;
        private readonly AutorService _autorService;

        public AutorServiceTests()
        {
            _autorRepositoryMock = new Mock<IAutorRepository>();
            _autorService = new AutorService(_autorRepositoryMock.Object);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameIsNull()
        {
            var result = await _autorService.ValidateNameAsync(null);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameIsEmpty()
        {
            var result = await _autorService.ValidateNameAsync("");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameContainsNumbers()
        {
            var result = await _autorService.ValidateNameAsync("Autor123");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameTooShort()
        {
            var result = await _autorService.ValidateNameAsync("Ab");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameTooLong()
        {
            var longName = new string('a', 201);

            var result = await _autorService.ValidateNameAsync(longName);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameAlreadyExists()
        {
            _autorRepositoryMock
                .Setup(x => x.ExistsByNameAsync("Autor Teste"))
                .ReturnsAsync(true);

            var result = await _autorService.ValidateNameAsync("Autor Teste");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldPass_WhenNameIsValid()
        {
            _autorRepositoryMock
                .Setup(x => x.ExistsByNameAsync("Autor Teste"))
                .ReturnsAsync(false);

            var result = await _autorService.ValidateNameAsync("Autor Teste");

            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateAutorAsync_ShouldFail_WhenAutorIsNull()
        {
            var result = await _autorService.CreateAutorAsync(null);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task CreateAutorAsync_ShouldCreateAutor_WhenValid()
        {
            var autor = new AutorEntity
            {
                Id = Guid.NewGuid(),
                Name = "Autor Teste"
            };

            _autorRepositoryMock
                .Setup(x => x.CreateAsync(autor))
                .ReturnsAsync(autor);

            var result = await _autorService.CreateAutorAsync(autor);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetAllAutoresAsync_ShouldReturnAutores()
        {
            var autores = new List<AutorEntity>
            {
                new AutorEntity { Id = Guid.NewGuid(), Name = "Autor 1" }
            };

            _autorRepositoryMock
                .Setup(x => x.GetAllAutoresAsync())
                .ReturnsAsync(autores);

            var result = await _autorService.GetAllAutoresAsync();

            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateAutorAsync_ShouldFail_WhenAutorNotFound()
        {
            var autor = new AutorEntity
            {
                Id = Guid.NewGuid(),
                Name = "Autor"
            };

            _autorRepositoryMock
                .Setup(x => x.UpdateAsync(autor))
                .ReturnsAsync((AutorEntity)null);

            var result = await _autorService.UpdateAutorAsync(autor);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateAutorAsync_ShouldUpdateAutor_WhenValid()
        {
            var autor = new AutorEntity
            {
                Id = Guid.NewGuid(),
                Name = "Autor Atualizado"
            };

            _autorRepositoryMock
                .Setup(x => x.UpdateAsync(autor))
                .ReturnsAsync(autor);

            var result = await _autorService.UpdateAutorAsync(autor);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteAutorByIdAsync_ShouldFail_WhenAutorNotFound()
        {
            var id = Guid.NewGuid();

            _autorRepositoryMock
                .Setup(x => x.DeleteAutorByIdAsync(id))
                .ReturnsAsync(false);

            var result = await _autorService.DeleteAutorByIdAsync(id);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteAutorByIdAsync_ShouldDeleteAutor_WhenExists()
        {
            var id = Guid.NewGuid();

            _autorRepositoryMock
                .Setup(x => x.DeleteAutorByIdAsync(id))
                .ReturnsAsync(true);

            var result = await _autorService.DeleteAutorByIdAsync(id);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetAutorByIdAsync_ShouldFail_WhenAutorNotFound()
        {
            var id = Guid.NewGuid();

            _autorRepositoryMock
                .Setup(x => x.GetAutorByIdAsync(id))
                .ReturnsAsync((AutorEntity)null);

            var result = await _autorService.GetAutorByIdAsync(id);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task GetAutorByIdAsync_ShouldReturnAutor_WhenExists()
        {
            var id = Guid.NewGuid();

            var autor = new AutorEntity
            {
                Id = id,
                Name = "Autor Teste"
            };

            _autorRepositoryMock
                .Setup(x => x.GetAutorByIdAsync(id))
                .ReturnsAsync(autor);

            var result = await _autorService.GetAutorByIdAsync(id);

            Assert.True(result.Success);
        }
    }
}