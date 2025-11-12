namespace TallerMecanico.Core.Interfaces
{
    public interface IDapperContext
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null);
    }
}
