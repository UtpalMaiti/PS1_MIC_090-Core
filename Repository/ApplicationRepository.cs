using Microsoft.Data.SqlClient;
using Dapper;

using PS1_MIC_090_Core.Models.Constants;
using PS1_MIC_090_Core.Repository.Domain;

namespace PS1_MIC_090_Core.Repository
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<Application>> QueryAsync(string sqlQuery);
    }
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        private readonly string? connectionString;

        public ApplicationRepository(string? connectionString = default):base(connectionString)
        {
            this.connectionString  = connectionString ?? DBConnect.APP_SQL;
        }

        public async Task<IEnumerable<Application>> QueryAsync(string sqlQuery)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return  await connection.QueryAsync<Application>(sqlQuery);
                
            }
        }
    }
}
