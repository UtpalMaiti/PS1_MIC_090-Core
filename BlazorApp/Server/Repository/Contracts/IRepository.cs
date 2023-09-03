using static Dapper.SqlMapper;

namespace BlazorApp.Server.Repository.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(int maxLimit = 100, string? connectionString = default);
        Task<T?> Get(int id, string? connectionString = default);
        Task<T> Add(T item, string? connectionString = default);
        Task<IEnumerable<T>> AddRange(IEnumerable<T> items, string? connectionString = default);
        Task<bool> Update(T item, string? connectionString = default);
        Task<bool> Delete(int id, string? connectionString = default);
        Task<IEnumerable<T>> QueryAsync(string sqlQuery, string? connectionString = default);
    }
}
