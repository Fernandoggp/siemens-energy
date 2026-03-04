using Project.Domain.Entities;
using Project.Api.ViewModel.Dto.Autor;

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
    }
}
