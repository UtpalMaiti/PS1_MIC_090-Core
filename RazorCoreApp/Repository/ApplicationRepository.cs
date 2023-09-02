using Microsoft.Data.SqlClient;
using Dapper;

using PS1_MIC_090_Core.Models.Constants;
using PS1_MIC_090_Core.Repository.Domain;
using PS1_MIC_090_Core.Repository.Contracts;

namespace PS1_MIC_090_Core.Repository
{
    public interface IApplicationRepository:IRepository<Application>
    {
       
    }
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        private string connectionString = AppConst.APP_SQL;

    }
}
