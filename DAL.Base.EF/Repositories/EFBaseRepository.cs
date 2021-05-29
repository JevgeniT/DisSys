using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Mappers;
using Contracts.DAL.Base.Repositories;
using Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class EFBaseRepository<TDbContext, TUser, TDomainEntity, TDALEntity> : 
        EFBaseRepository<Guid, TDbContext, TUser, TDomainEntity, TDALEntity>, IBaseRepository<TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<Guid>, new()
        where TDomainEntity : class, IDomainEntityBaseMetadata<Guid>, new()
        where TDbContext : DbContext,  IBaseEntityTracker
        where TUser : IdentityUser<Guid>
    {
        public EFBaseRepository(TDbContext dbContext,  IBaseDALMapper<TDomainEntity, TDALEntity> mapper) 
            : base(dbContext, mapper)
        { }
    }

    public class EFBaseRepository<TKey, TDbContext,TUser, TDomainEntity, TDALEntity> 
        : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<TKey>, new()
        where TDomainEntity : class, IDomainEntityBaseMetadata<TKey>, new()
        where TDbContext : DbContext, IBaseEntityTracker<TKey>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>

    {
        protected TDbContext RepoDbContext;
        protected DbSet<TDomainEntity> RepoDbSet;
        protected IBaseDALMapper<TDomainEntity, TDALEntity> Mapper;
        
        public EFBaseRepository(TDbContext dbContext, IBaseDALMapper<TDomainEntity, TDALEntity> mapper)
        {
            RepoDbContext = dbContext;
            RepoDbSet = RepoDbContext.Set<TDomainEntity>() ??
                        throw new ArgumentNullException(typeof(TDALEntity).Name + " was not found as DBSet!");
            Mapper = mapper;
           
        }

        public virtual TDALEntity Add(TDALEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map<TDALEntity, TDomainEntity>(entity);
            
            if (userId is not null) CheckDomainEntityOwnership(dalEntity, userId);
            
            var trackedEntity = RepoDbSet.Add(dalEntity).Entity;
            
            RepoDbContext.AddToEntityTracker(trackedEntity,dalEntity);
            
            return Mapper.Map(trackedEntity);
        }

        public async Task<IEnumerable<TDALEntity>> AddRangeAsync(IEnumerable<TDALEntity> entities)
        {
            var dalEntities = entities.Select(entity => Mapper.Map<TDALEntity, TDomainEntity>(entity));
            
            await RepoDbContext.AddRangeAsync(dalEntities);

            return dalEntities.Select(entity => Mapper.Map(entity));

        }

        public virtual async Task<IEnumerable<TDALEntity>> AllAsync(object? userId = null)
        {
            var query = PrepareQuery(userId);
            var entities = (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
            return entities;
        }
        
        public virtual async Task<TDALEntity> FirstOrDefaultAsync(TKey id, object? userId = null)
        {
            var query = PrepareQuery(userId);
            var domain = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            return Mapper.Map(domain);
        }


        public virtual async Task<TDALEntity> UpdateAsync(TDALEntity entity, object? userId = null)
        {
            var domainEntity = Mapper.Map<TDALEntity, TDomainEntity>(entity);
            await CheckDomainEntityOwnership(domainEntity, userId);
            var trackedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
            return Mapper.Map(trackedDomainEntity);
         }

        public virtual async Task<TDALEntity> RemoveAsync(TDALEntity entity, object? userId = null)
        {
            var domainEntity = Mapper.Map<TDALEntity, TDomainEntity>(entity);
            await CheckDomainEntityOwnership(domainEntity, userId);
            return Mapper.Map(RepoDbSet.Remove(domainEntity).Entity);
        }

        public virtual async Task <TDALEntity> RemoveAsync(TKey id, object? userId = null)
        {
            var query = PrepareQuery(userId);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id)) ??
                               throw new LogicException("Entity to be updated was not found in data source!");
            await CheckDomainEntityOwnership(domainEntity, userId);
            return Mapper.Map(RepoDbSet.Remove(domainEntity).Entity);
        }

        public virtual async Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            var query = PrepareQuery(userId);
            return await query.AnyAsync(e => e.Id.Equals(id));
        }

        protected IQueryable<TDomainEntity> PrepareQuery(object? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            query.AsNoTracking();
            if (userId is not null && typeof(IDomainEntityUser<TKey, TUser>).IsAssignableFrom(typeof(TDomainEntity)))
            {
                query = query.Where(e =>
                    Microsoft.EntityFrameworkCore.EF.Property<TKey>(e, nameof(IDomainEntityUser<TKey, TUser>.AppUserId))
                        .Equals((TKey) userId));
            }
            
            return query;
        }
        
        protected async Task CheckDomainEntityOwnership(TDomainEntity entity, object? userId = null)
        {
            if (!await ExistsAsync(entity.Id, userId))
                throw new UnauthorizedException("Entity to be updated was not found in data source!");
        }
    }
}
