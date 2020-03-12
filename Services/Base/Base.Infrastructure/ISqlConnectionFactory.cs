using System.Data;

namespace Base.Infrastructure
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}