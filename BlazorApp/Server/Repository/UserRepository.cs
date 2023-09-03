using BlazorApp.Server.Data;
using BlazorApp.Server.Models.Constants;
using BlazorApp.Server.Repository.Contracts;
using BlazorApp.Server.Repository.Core;
using BlazorApp.Server.Repository.Domain;

using Microsoft.EntityFrameworkCore;

namespace PS1_MIC_090_Core.Repository
{
    public class UserRepository : Repository<AspNetUser>, IUserRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private string connectionString = AppConst.APP_SQL;

        public UserRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task< IEnumerable<AspNetUser>> GetAllByUsername(string username, string conSt)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new CoreDbContext(CoreDbContext.GetOptions(this.connectionString)))
            {
                var users= await ctx.Users.Where(x => x.UserName!.Equals(username)).ToListAsync();

                return await Task.FromResult( users);
            }
        }

        public async Task<AspNetUser?> GetByUsername(string username, string conSt)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new CoreDbContext(CoreDbContext.GetOptions(this.connectionString)))
            {
               var user = await ctx.Users.FirstOrDefaultAsync(x => x.UserName!.Equals(username!));

                return await Task.FromResult(user);
            }
        }

        public bool ValidateLastChanged(string lastChanged)
        {
            return false;
        }
    }
}
