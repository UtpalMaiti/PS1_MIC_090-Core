using PS1_MIC_090_Core.Models.Constants;
using PS1_MIC_090_Core.Repository.Contracts;
using PS1_MIC_090_Core.Repository.Domain;

namespace PS1_MIC_090_Core.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllByUsername(string username, string conSt);
        User GetByUsername(string username, string conSt);
        bool ValidateLastChanged(string lastChanged);
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private string connectionString = AppConst.APP_SQL;

        public UserRepository(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<User> GetAllByUsername(string username, string conSt)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new AppDbContext(constr))
            {

                return ctx.Users.Where(x => x.UserName.Equals(username)).ToList();
            }
        }

        public User GetByUsername(string username, string conSt)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new AppDbContext(constr))
            {
                return ctx.Users.FirstOrDefault(x => x.UserName.Equals(username));
            }
        }

        public bool ValidateLastChanged(string lastChanged)
        {
            return false;
        }
    }
}
