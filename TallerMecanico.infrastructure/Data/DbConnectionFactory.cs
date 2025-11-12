
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using TallerMecanico.Core.Interfaces;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("Default");
        return new MySqlConnection(connectionString);
    }
}
