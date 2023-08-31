using PS1_MIC_090_Core.Models.Constants;

namespace PS1_MIC_090_Core.Repository.Contracts
{
    public abstract class BaseRepository
    {
        public readonly string ConnectionString = AppConst.APP_SQL;
        protected BaseRepository() { }
    }
}
