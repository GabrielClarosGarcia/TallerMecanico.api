using System.Data;

namespace TallerMecanico.Core.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
