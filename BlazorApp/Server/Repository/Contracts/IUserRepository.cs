using BlazorApp.Server.Repository.Core;

using System.Threading.Tasks;

namespace BlazorApp.Server.Repository.Contracts
{
    public interface IUserRepository : IRepository<AspNetUser>
    {
        Task<IEnumerable<AspNetUser>> GetAllByUsername(string username, string conSt);
        Task<AspNetUser?> GetByUsername(string username, string conSt);
        bool ValidateLastChanged(string lastChanged);
    }
}
