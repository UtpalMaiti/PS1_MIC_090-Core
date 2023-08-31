using Microsoft.EntityFrameworkCore;

using PS1_MIC_090_Core.Models.Constants;
using PS1_MIC_090_Core.Repository.Contracts;
using PS1_MIC_090_Core.Repository.Domain;

using System;
using System.Collections;
using System.Collections.Generic;

namespace PS1_MIC_090_Core.Repository
{
    public class Repository<TEntity> : BaseRepository, IRepository<TEntity> where TEntity : class
    {
        private readonly string? connectionString;

        public Repository(string? connectionString = default)
        {
            this.connectionString = connectionString ?? AppConst.APP_SQL;
        }

        public async Task<TEntity> Add(TEntity item, string? conSt = null)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new AppDbContext(AppDbContext.GetOptions(constr)))
            {

                if (item is not null)
                {
                    await ctx.Set<TEntity>().AddAsync(item);
                    await ctx.SaveChangesAsync();

                }
            }
            return await Task.FromResult(item!);
        }


        public async Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> items, string? conSt = null)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new AppDbContext(AppDbContext.GetOptions(constr)))
            {
             
                if (items is not null)
                {
                    await ctx.Set<TEntity>().AddRangeAsync(items);
                    await ctx.SaveChangesAsync();
                    
                }
            }
            return await Task.FromResult(items!);
        }

        public async Task<bool> Delete(int id, string? conSt = null)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new AppDbContext(AppDbContext.GetOptions(constr)))
            {
                var item= await ctx.Set<TEntity>().FindAsync(id);
                if (item is not null)
                {
                    ctx.Set<TEntity>().Remove(item);
                    await ctx.SaveChangesAsync();
                }
                return await Task.FromResult(true);     
            }
        }
        public async Task<TEntity?> Get(int id, string? conSt = null)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new AppDbContext(AppDbContext.GetOptions(constr)))
            {
                return await ctx.Set<TEntity>().FindAsync(id);
            }
        }

        public  async Task<IEnumerable<TEntity>> GetAll(int maxLimit = 100, string? conSt = default)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new AppDbContext(AppDbContext.GetOptions(constr)))
            {
                    
                return await ctx.Set<TEntity>().Take(maxLimit).ToListAsync();
            }
        }

        public async Task<bool> Update(TEntity item, string? conSt = null)
        {
            string constr = conSt ?? ConnectionString;
            using (var ctx = new AppDbContext(AppDbContext.GetOptions(constr)))
            {
                if (item is not null)
                {
                    ctx.Set<TEntity>().Update(item);
                    await ctx.SaveChangesAsync();
                }
                return await Task.FromResult(true);
            }
        }

    }
}
