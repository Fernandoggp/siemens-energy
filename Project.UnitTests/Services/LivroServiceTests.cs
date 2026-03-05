using Moq;
using Moq.AutoMock;
using Project.Application.Services;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;

namespace Project.UnitTests.Services
{
    public class LivroServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly LivroService _livroService;

        public LivroServiceTests()
        {
            _mocker = new AutoMocker();
            _livroService = _mocker.CreateInstance<LivroService>();
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameIsEmpty()
        {
            var result = await _livroService.ValidateNameAsync("");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameContainsInvalidCharacters()
        {
            var result = await _livroService.ValidateNameAsync("Livro123");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameLengthInvalid()
        {
            var result = await _livroService.ValidateNameAsync("AB");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldFail_WhenNameAlreadyExists()
        {
            var name = "Livro Teste";

            _mocker.GetMock<ILivroRepository>()
                .Setup(x => x.ExistsByNameAsync(name))
                .ReturnsAsync(true);

            var result = await _livroService.ValidateNameAsync(name);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ValidateNameAsync_ShouldReturnOk_WhenNameValid()
        {
            var name = "Livro Teste";

            _mocker.GetMock<ILivroRepository>()
                .Setup(x => x.ExistsByNameAsync(name))
                .ReturnsAsync(false);

            var result = await _livroService.ValidateNameAsync(name);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateLivroAsync_ShouldFail_WhenLivroIsNull()
        {
            var result = await _livroService.CreateLivroAsync(null);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task CreateLivroAsync_ShouldCreateLivro_WhenValid()
        {
            var livro = new LivroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Livro Teste"
            };

            _mocker.GetMock<ILivroRepository>()
                .Setup(x => x.CreateAsync(livro))
                .ReturnsAsync(livro);

            var result = await _livroService.CreateLivroAsync(livro);

            Assert.True(result.Success);

            _mocker.GetMock<ILivroRepository>()
                .Verify(x => x.CreateAsync(livro), Times.Once);
        }

        [Fact]
        public async Task GetAllLivrosAsync_ShouldReturnLivros()
        {
            var livros = new List<LivroEntity>();

            _mocker.GetMock<ILivroRepository>()
                .Setup(x => x.GetAllLivrosAsync())
                .ReturnsAsync(livros);

            var result = await _livroService.GetAllLivrosAsync();

            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetFilteredLivrosAsync_ShouldReturnFilteredLivros()
        {
            var livros = new List<LivroEntity>();

            _mocker.GetMock<ILivroRepository>()
                .Setup(x => x.GetFilteredLivrosAsync(null, null, null))
                .ReturnsAsync(livros);

            var result = await _livroService.GetFilteredLivrosAsync(null, null, null);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateLivroAsync_ShouldFail_WhenLivroNotFound()
        {
            var livro = new LivroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Livro Teste"
            };

            _mocker.GetMock<ILivroRepository>()
                .Setup(x => x.UpdateAsync(livro))
                .ReturnsAsync((LivroEntity)null);

            var result = await _livroService.UpdateLivroAsync(livro);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateLivroAsync_ShouldUpdateLivro_WhenFound()
        {
            var livro = new LivroEntity
            {
                Id = Guid.NewGuid(),
                Name = "Livro Teste"
            };

            _mocker.GetMock<ILivroRepository>()
                .Setup(x => x.UpdateAsync(livro))
                .ReturnsAsync(livro);

            var result = await _livroService.UpdateLivroAsync(livro);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteLivroByIdAsync_ShouldFail_WhenLivroNotFound()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<ILivroRepository>()
                .Setup(x => x.DeleteLivroByIdAsync(id))
                .ReturnsAsync(false);

            var result = await _livroService.DeleteLivroByIdAsync(id);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteLivroByIdAsync_ShouldDeleteLivro_WhenFound()
        {
            var id = Guid.NewGuid();

            _mocker.GetMock<ILivroRepository>()
                .Setup(x => x.DeleteLivroByIdAsync(id))
                .ReturnsAsync(true);

            var result = await _livroService.DeleteLivroByIdAsync(id);

            Assert.True(result.Success);
        }
    }
}