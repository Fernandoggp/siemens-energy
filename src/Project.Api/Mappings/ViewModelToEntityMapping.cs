using Project.Domain.Entities;
using Project.Api.ViewModel.Dto.Autor;
using Project.Api.ViewModel.Dto.Genero;
using Project.Api.ViewModel.Dto.Livro;

namespace Project.Api.Mappings
{
    public class ViewModelToEntityMapping
    {
        public static AutorEntity MapCreateAutorDtoToEntity(CreateAutorDto dto)
        {
            return new AutorEntity
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
        }

        public static AutorEntity MapUpdateAutorDtoToEntity(UpdateAutorDto dto)
        {
            return new AutorEntity
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static GeneroEntity MapCreateGeneroDtoToEntity(CreateGeneroDto dto)
        {
            return new GeneroEntity
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
        }

        public static GeneroEntity MapUpdateGeneroDtoToEntity(UpdateGeneroDto dto)
        {
            return new GeneroEntity
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static LivroEntity MapCreateLivroDtoToEntity(CreateLivroDto dto)
        {
            return new LivroEntity
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                AutorId = dto.AutorId,
                GeneroId = dto.GeneroId
            };
        }

        public static LivroEntity MapUpdateLivroDtoToEntity(UpdateLivroDto dto)
        {
            return new LivroEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                AutorId = dto.AutorId,
                GeneroId = dto.GeneroId
            };
        }
    }
}
