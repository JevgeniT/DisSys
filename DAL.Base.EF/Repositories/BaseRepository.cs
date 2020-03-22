using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public   class BaseRepository<TEntity>: BaseRepository<TEntity, int> 
        where TEntity : class, IDomainEntity<int>, new()
    {
        public   BaseRepository(DbContext dbContext) : base(dbContext)
        {
            
        }
    }

    public   class BaseRepository<TEntity, Tkey> : IBaseRepository<TEntity, Tkey>
        where TEntity : class, IDomainEntity<Tkey>, new()
        where Tkey : struct, IComparable
    {
        protected DbSet<TEntity> RepoDbSet;
        protected DbContext RepoDbContext;
        
        public   BaseRepository(DbContext dbContext)
        {
            RepoDbContext = dbContext;
            RepoDbSet = RepoDbContext.Set<TEntity>();
            if (RepoDbSet == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name+ "was not found as DbSet");
            }
        }
        
        public virtual IEnumerable<TEntity> All()
        {
            return RepoDbSet.ToList();
        }

        public virtual async  Task<IEnumerable<TEntity>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public virtual TEntity Find(params object[] id)
        {
            return   RepoDbSet.Find(id);
        }

        public virtual async  Task<TEntity> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return  RepoDbSet.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return   RepoDbSet.Update(entity).Entity;

        }

        public virtual TEntity Remove(TEntity entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public virtual TEntity Remove(params object[] id)
        {
            
            return RepoDbSet.Remove(Find(id)).Entity;
        }

        public virtual int SaveChanges()
        {
            return RepoDbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await RepoDbContext.SaveChangesAsync();
        }
    }
}