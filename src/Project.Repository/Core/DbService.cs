using Dapper;
using Project.Repository.Configurations;
using Npgsql;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Project.Repository.Core
{
    [ExcludeFromCodeCoverage]
    public class DbService : IDbService
    {
        private readonly DbConfig _configuration;

        private const string CONNECTION_STRING_ERROR = "A conexão do Sql não foi informada";
        private const string GENERIC_ERROR = "Houve um problema no serviço de banco de dados.";

        public string Schema => _configuration.Schema;
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }

        public DbService(DbConfig configuration)
        {
            _configuration = configuration;
            if (string.IsNullOrEmpty(_configuration.CapitalControlDbConnection))
                throw new ArgumentNullException(CONNECTION_STRING_ERROR);

        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_configuration.CapitalControlDbConnection);

        public async Task<IEnumerable<TOutput>> ExecuteQueryRequestAsync<TOutput>(Request queryRequest)
        {
            var result = default(IEnumerable<TOutput>);
            var connection = Transaction?.Connection ?? CreateConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                result = await connection.QueryAsync<TOutput>(
                    queryRequest.Sql,
                    queryRequest.DynamicParameters, Transaction);

                if (result is null || result.AsList().Count == 0)
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                if (Transaction == null)
                {
                    connection.Close();
                }
            }

            return result;
        }

        public async Task<TOutput> ExecuteQueryFirstOrDefaultAsync<TOutput>(Request queryRequest)
        {
            var result = default(TOutput);
            var connection = Transaction?.Connection ?? CreateConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                result = await connection.QueryFirstOrDefaultAsync<TOutput>(
                    queryRequest.Sql,
                    queryRequest.DynamicParameters, Transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                if (Transaction == null)
                {
                    connection.Close();
                }
            }

            return result;
        }

        public async Task ExecuteCommandRequestAsync(Request commandRequest)
        {
            var connection = Transaction?.Connection ?? CreateConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var localTransaction = Transaction ?? connection.BeginTransaction();

                try
                {
                    await connection.ExecuteAsync(
                        commandRequest.Sql,
                        commandRequest.DynamicParameters,
                        localTransaction);

                    if (Transaction == null)
                    {
                        localTransaction.Commit();
                    }
                }
                catch (Exception)
                {
                    if (Transaction == null)
                    {
                        localTransaction.Rollback();
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                if (Transaction == null)
                {
                    connection.Close();
                }
            }
        }

        public async Task ExecuteCommandRequestAsync(IEnumerable<Request> commandRequests)
        {
            var connection = Transaction?.Connection ?? CreateConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var localTransaction = Transaction ?? connection.BeginTransaction();

                try
                {
                    foreach (var commandRequest in commandRequests)
                    {
                        await connection.ExecuteAsync(
                            commandRequest.Sql,
                            commandRequest.DynamicParameters,
                            localTransaction);
                    }

                    if (Transaction == null)
                    {
                        localTransaction.Commit();
                    }
                }
                catch (Exception)
                {
                    if (Transaction == null)
                    {
                        localTransaction.Rollback();
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                if (Transaction == null)
                {
                    connection.Close();
                }
            }
        }

        public async Task<int> ExecuteCommandRowsRequestAsync(IEnumerable<Request> commandRequests)
        {
            int affectedLines = 0;
            var connection = CreateConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var localTransaction = Transaction ?? connection.BeginTransaction();

                try
                {
                    foreach (var commandRequest in commandRequests)
                    {
                        affectedLines += await connection.ExecuteAsync(
                            commandRequest.Sql,
                            commandRequest.DynamicParameters,
                            localTransaction);
                    }

                    if (Transaction == null)
                    {
                        localTransaction.Commit();
                    }
                }
                catch (Exception)
                {
                    if (Transaction == null)
                    {
                        localTransaction.Rollback();
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                if (Transaction == null)
                {
                    connection.Close();
                }
            }

            return affectedLines;
        }

        public async Task ExecuteProcedureRequestAsync(
            IEnumerable<Request> commandRequests,
            IDbTransaction? transaction = null,
            int? commandTimeout = null
            )
        {
            var connection = CreateConnection();

            try
            {
                connection.Open();
                foreach (var commandRequest in commandRequests)
                {
                    await connection.ExecuteAsync(
                        commandRequest.Sql,
                        commandRequest.DynamicParameters,
                        transaction,
                        commandTimeout,
                        CommandType.StoredProcedure
                        );
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<IEnumerable<TOutput>> ExecuteProcedureRequestAsync<TOutput>(
            Request commandRequest,
            IDbTransaction? transaction = null,
            int? commandTimeout = null
            )
        {
            var result = default(IEnumerable<TOutput>);
            var connection = CreateConnection();

            try
            {
                connection.Open();
                result = await connection.QueryAsync<TOutput>(
                    commandRequest.Sql,
                    commandRequest.DynamicParameters,
                    transaction,
                    commandTimeout,
                    CommandType.StoredProcedure
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }


    }
}