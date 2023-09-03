using BlazorApp.Server.Data;
using BlazorApp.Server.Models.Constants;
using BlazorApp.Server.Repository.Contracts;

using Dapper;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections;
using System.Collections.Generic;

namespace BlazorApp.Server.Repository.Domain
{
    public class Repository<TEntity> : BaseRepository, IRepository<TEntity>, IDisposable where TEntity : class
    {
        private string connectionString = AppConst.APP_SQL;

        ~Repository()
        {
            Dispose();
        }

        public async Task<TEntity> Add(TEntity item, string? connectionString = default)
        {
            this.connectionString = connectionString ?? AppConst.APP_SQL;
            using (var ctx = new CoreDbContext(CoreDbContext.GetOptions(this.connectionString)))
            {

                if (item is not null)
                {
                    await ctx.Set<TEntity>().AddAsync(item);
                    await ctx.SaveChangesAsync();

                }
            }
            return await Task.FromResult(item!);
        }


        public async Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> items, string? connectionString = default)
        {
            this.connectionString = connectionString ?? AppConst.APP_SQL;
            using (var ctx = new CoreDbContext(CoreDbContext.GetOptions(this.connectionString)))
            {
                if (items is not null)
                {
                    await ctx.Set<TEntity>().AddRangeAsync(items);
                    await ctx.SaveChangesAsync();

                }
            }
            return await Task.FromResult(items!);
        }

        public async Task<bool> Delete(int id, string? connectionString = default)
        {
            this.connectionString = connectionString ?? AppConst.APP_SQL;
            using (var ctx = new CoreDbContext(CoreDbContext.GetOptions(this.connectionString)))
            {
                var item = await ctx.Set<TEntity>().FindAsync(id);
                if (item is not null)
                {
                    ctx.Set<TEntity>().Remove(item);
                    await ctx.SaveChangesAsync();
                }
                return await Task.FromResult(true);

            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        public async Task<TEntity?> Get(int id, string? connectionString = default)
        {

            this.connectionString = connectionString ?? AppConst.APP_SQL;
            using (var ctx = new CoreDbContext(CoreDbContext.GetOptions(this.connectionString)))
            {
                return await ctx.Set<TEntity>().FindAsync(id);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll(int maxLimit = 100, string? connectionString = default)
        {
            this.connectionString = connectionString ?? AppConst.APP_SQL;
            using (var ctx = new CoreDbContext(CoreDbContext.GetOptions(this.connectionString)))
            {

                return await ctx.Set<TEntity>().Take(maxLimit).ToListAsync();
            }
        }

        public async Task<bool> Update(TEntity item, string? connectionString = default)
        {
            this.connectionString = connectionString ?? AppConst.APP_SQL;
            using (var ctx = new CoreDbContext(CoreDbContext.GetOptions(this.connectionString)))
            {
                if (item is not null)
                {
                    ctx.Set<TEntity>().Update(item);
                    await ctx.SaveChangesAsync();
                }
                return await Task.FromResult(true);
            }
        }
        public async Task<IEnumerable<TEntity>> QueryAsync(string sqlQuery, string? connectionString = default)
        {
            this.connectionString = connectionString ?? AppConst.APP_SQL;
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<TEntity>(sqlQuery);

            }
        }

    }
}
