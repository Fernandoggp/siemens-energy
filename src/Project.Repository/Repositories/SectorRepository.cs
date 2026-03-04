using Project.Domain.Interfaces.Repositories;
using Project.Repository.Base;
using Project.Domain.Models;
using Project.Repository.Core;
using Project.Repository.Builders.Sector;

namespace Project.Repository.Repositories
{
    public class SectorRepository : RepositoryBase, ISectorRepository
    {
        private readonly ISectorBuilder _sectorBuilder;

        public SectorRepository(IDbService dbService, ISectorBuilder sectorBuilder) : base(dbService)
        {
            _sectorBuilder = sectorBuilder;
        }

        public async Task<dynamic> ExecuteAsync()
        {
            var request = _sectorBuilder.FindAllSectors();
            var results = await _dbService.ExecuteQueryRequestAsync<dynamic>(request);

            return results.Select(item => new SectorDbModel(
                item.name,
                item.perennial
            )).ToList();
        }
    }
}
