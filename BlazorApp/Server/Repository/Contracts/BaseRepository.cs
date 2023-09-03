

using BlazorApp.Server.Models.Constants;

namespace BlazorApp.Server.Repository.Contracts
{
    public abstract class BaseRepository
    {
        public readonly string ConnectionString = AppConst.APP_SQL;
        protected BaseRepository() { }
    }
}
