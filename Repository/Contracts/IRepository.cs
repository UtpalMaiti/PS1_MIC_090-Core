using static Dapper.SqlMapper;

namespace PS1_MIC_090_Core.Repository.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(int maxLimit = 100, string? conSt = default);
        Task<T?> Get(int id, string? conSt = default);
        Task<T> Add(T item, string? conSt = default);
        Task<IEnumerable<T>> AddRange(IEnumerable<T> items, string? conSt = null);
        Task<bool> Update(T item, string? conSt = default);
        Task<bool> Delete(int id, string? conSt = default);
    }
}
