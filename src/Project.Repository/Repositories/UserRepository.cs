using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Repository.Base;
using Project.Repository.Builders.User;
using Project.Repository.Core;

namespace Project.Repository.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly IUserBuilder _userBuilder;

        public UserRepository(IDbService dbService, IUserBuilder userBuilder) : base(dbService)
        {
            _userBuilder = userBuilder;
        }

        public async Task<dynamic> CreateAsync(UserEntity newUser)
        {
            var request = _userBuilder.CreateUser(newUser);
            var results = await _dbService.ExecuteQueryRequestAsync<dynamic>(request);

            return results;
        }

        public async Task<dynamic> GetUserByIdAsync(string id)
        {
            var request = _userBuilder.GetUserById(id);
            var results = await _dbService.ExecuteQueryRequestAsync<dynamic>(request);

            return results;
        }

        public async Task<dynamic> GetUserByEmailAsync(string email)
        {
            var request = _userBuilder.GetUserByEmail(email);
            var results = await _dbService.ExecuteQueryRequestAsync<dynamic>(request);
            if (results == null)
            {
                return null;
            }
            var item = results.FirstOrDefault();

            return item;
        }

        public async Task<dynamic> GetUserByCpfCnpjAsync(string cpfCnpj)
        {
            var request = _userBuilder.GetUserByCpfCnpj(cpfCnpj);
            var results = await _dbService.ExecuteQueryRequestAsync<dynamic>(request);
            if (results == null)
            {
                return null;
            }
            var item = results.FirstOrDefault();

            return item;
        }
    }
}
