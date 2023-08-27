using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

using PS1_MIC_090_Core.Models.Constants;
using PS1_MIC_090_Core.Repository.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace PS1_MIC_090_Core.Repository
{
    public class AppDbContext : DbContext
    {
        private readonly string connectionString;

        public AppDbContext(string? connectionString = default) : base(GetOptions(connectionString: connectionString!))
        {
            this.connectionString = connectionString ?? DBConnect.APP_SQL;
        }

        protected AppDbContext() : base(GetOptions(connectionString: DBConnect.APP_SQL))
        {
            this.connectionString = connectionString ?? DBConnect.APP_SQL;
        }

        public AppDbContext(DbContextOptions options) : base(GetOptions(connectionString: DBConnect.APP_SQL))
        {
            this.connectionString = connectionString ?? DBConnect.APP_SQL;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        public static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<NameSuffix> NameSuffixes { get; set; }

        public override DatabaseFacade Database => base.Database;

        public override ChangeTracker ChangeTracker => base.ChangeTracker;

        public override IModel Model => base.Model;

        public override DbContextId ContextId => base.ContextId;

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            return base.Add(entity);
        }

        public override EntityEntry Add(object entity)
        {
            return base.Add(entity);
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity, cancellationToken);
        }

        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity, cancellationToken);
        }

        public override void AddRange(params object[] entities)
        {
            base.AddRange(entities);
        }

        public override void AddRange(IEnumerable<object> entities)
        {
            base.AddRange(entities);
        }

        public override Task AddRangeAsync(params object[] entities)
        {
            return base.AddRangeAsync(entities);
        }

        public override Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
        {
            return base.AddRangeAsync(entities, cancellationToken);
        }

        public override EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
        {
            return base.Attach(entity);
        }

        public override EntityEntry Attach(object entity)
        {
            return base.Attach(entity);
        }

        public override void AttachRange(params object[] entities)
        {
            base.AttachRange(entities);
        }

        public override void AttachRange(IEnumerable<object> entities)
        {
            base.AttachRange(entities);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }

        public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        {
            return base.Entry(entity);
        }

        public override EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }


        public override object? Find([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] Type entityType, params object?[]? keyValues)
        {
            return base.Find(entityType, keyValues);
        }

        public override TEntity? Find<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>(params object?[]? keyValues) where TEntity : class
        {
            return base.Find<TEntity>(keyValues);
        }

        public override ValueTask<object?> FindAsync([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] Type entityType, params object?[]? keyValues)
        {
            return base.FindAsync(entityType, keyValues);
        }

        public override ValueTask<object?> FindAsync([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] Type entityType, object?[]? keyValues, CancellationToken cancellationToken)
        {
            return base.FindAsync(entityType, keyValues, cancellationToken);
        }

        public override ValueTask<TEntity?> FindAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>(params object?[]? keyValues) where TEntity : class
        {
            return base.FindAsync<TEntity>(keyValues);
        }

        public override ValueTask<TEntity?> FindAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>(object?[]? keyValues, CancellationToken cancellationToken) where TEntity : class
        {
            return base.FindAsync<TEntity>(keyValues, cancellationToken);
        }

        public override IQueryable<TResult> FromExpression<TResult>(Expression<Func<IQueryable<TResult>>> expression)
        {
            return base.FromExpression(expression);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            return base.Remove(entity);
        }

        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }

        public override void RemoveRange(params object[] entities)
        {
            base.RemoveRange(entities);
        }

        public override void RemoveRange(IEnumerable<object> entities)
        {
            base.RemoveRange(entities);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }



        public override DbSet<TEntity> Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>(string name)
        {
            return base.Set<TEntity>(name);
        }


        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            return base.Update(entity);
        }

        public override EntityEntry Update(object entity)
        {
            return base.Update(entity);
        }

        public override void UpdateRange(params object[] entities)
        {
            base.UpdateRange(entities);
        }

        public override void UpdateRange(IEnumerable<object> entities)
        {
            base.UpdateRange(entities);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>().ToTable("User").HasIndex(x => x.UserId).IsClustered().IsUnique();

            modelBuilder.Entity<Role>()
               .ToTable("Role")
               .Property(p => p.RoleId).IsRequired();

            modelBuilder.Entity<UserRoleMapping>()
           .ToTable("UserRoleMapping")
           .Property(p => p.UserRoleMappingId).IsRequired();

            modelBuilder.Entity<Relation>()
              .ToTable("Relation")
              .Property(p => p.RelationId).IsRequired();

            modelBuilder.Entity<Application>()
            .ToTable("Application")
            .Property(p => p.ApplicationId).IsRequired();

            modelBuilder.Entity<ApplicationStatus>()
           .ToTable("ApplicationStatus")
           .Property(p => p.ApplicationStatusId).IsRequired();

            modelBuilder.Entity<NameSuffix>()
           .ToTable("NameSuffix")
           .Property(p => p.SuffixId).IsRequired();

            base.OnModelCreating(modelBuilder);
        }


    }
}
