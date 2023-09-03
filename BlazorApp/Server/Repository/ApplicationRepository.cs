using Microsoft.Data.SqlClient;
using Dapper;

using BlazorApp.Server.Models.Constants;
using BlazorApp.Server.Repository.Domain;
using BlazorApp.Server.Repository.Contracts;

namespace PS1_MIC_090_Core.Repository
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        private string connectionString = AppConst.APP_SQL;

    }
}
