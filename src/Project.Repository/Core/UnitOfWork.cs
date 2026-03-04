using Project.Repository.Core;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IDbService _session;

    public UnitOfWork(IDbService session)
    {
        _session = session;
    }

    public void BeginTransaction()
    {
        _session.Connection = _session.CreateConnection();
        _session.Connection.Open();
        _session.Transaction = _session.Connection.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _session.Transaction?.Commit();
        }
        finally
        {
            Dispose();
        }
    }

    public void Rollback()
    {
        try
        {
            _session.Transaction?.Rollback();
        }
        finally
        {
            Dispose();
        }
    }

    public void Dispose()
    {
        _session.Transaction?.Dispose();
        _session.Connection?.Close();
        _session.Connection?.Dispose();
        _session.Transaction = null;
        _session.Connection = null;
    }
}