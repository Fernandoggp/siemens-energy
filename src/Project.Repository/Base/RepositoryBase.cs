using Project.Repository.Core;

namespace Project.Repository.Base
{
    public abstract class RepositoryBase
    {
        protected readonly IDbService _dbService;

        protected string Schema => _dbService.Schema;

        public RepositoryBase(
            IDbService dbService
            )
        {
            _dbService = dbService;
        }
    }
}
