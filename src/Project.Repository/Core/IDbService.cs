using System.Data;

namespace Project.Repository.Core
{
    public interface IDbService
    {
        string Schema { get; }
        IDbTransaction Transaction { get; set; }
        IDbConnection Connection { get; set; }

        IDbConnection CreateConnection();

        Task<IEnumerable<TOutput>> ExecuteQueryRequestAsync<TOutput>(Request queryRequest);

        Task<TOutput> ExecuteQueryFirstOrDefaultAsync<TOutput>(Request queryRequest);

        Task ExecuteCommandRequestAsync(Request commandRequests);

        Task ExecuteCommandRequestAsync(IEnumerable<Request> commandRequests);

        Task<int> ExecuteCommandRowsRequestAsync(IEnumerable<Request> commandRequests);

        Task ExecuteProcedureRequestAsync(
            IEnumerable<Request> commandRequests,
            IDbTransaction? transaction = null,
            int? commandTimeout = null
            );

        Task<IEnumerable<TOutput>> ExecuteProcedureRequestAsync<TOutput>(
            Request commandRequest,
            IDbTransaction? transaction = null,
            int? commandTimeout = null
            );
    }
}
