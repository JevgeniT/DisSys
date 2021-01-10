using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.Base.Mappers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace BLL.Base.Services
{

    public class BaseEntityService<TServiceRepository, TUnitOfWork, TDALEntity, TBLLEntity> : 
        BaseEntityService<Guid, TServiceRepository, TUnitOfWork, TDALEntity, TBLLEntity>, IBaseEntityService<TBLLEntity>
        where TBLLEntity : class, IDomainBaseEntity<Guid>, new()
        where TDALEntity : class, IDomainBaseEntity<Guid>, new()
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
        where TServiceRepository : IBaseRepository<TDALEntity>
    {
        public BaseEntityService( TUnitOfWork unitOfWork , IBaseBLLMapper<TDALEntity,TBLLEntity> mapper, TServiceRepository serviceRepository )
            :base(unitOfWork, mapper, serviceRepository)
        { }
    }
    

    public class BaseEntityService<TKey, TServiceRepository, TUnitOfWork, TDALEntity, TBLLEntity> 
        : BaseService, IBaseEntityService<TKey,TBLLEntity>
        where TKey : IEquatable<TKey>
        where TBLLEntity : class, IDomainBaseEntity<TKey>, new()
        where TDALEntity : class, IDomainBaseEntity<TKey>, new()
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker<TKey>
        where TServiceRepository : IBaseRepository<TKey,TDALEntity>
    {       
        // ReSharper disable MemberCanBePrivate.Global

        protected readonly TUnitOfWork ServiceUnitOfWork;
        protected readonly IBaseBLLMapper<TDALEntity, TBLLEntity> Mapper;
        protected readonly TServiceRepository ServiceRepository;
        // ReSharper enable MemberCanBePrivate.Global

        // ReSharper disable once MemberCanBeProtected.Global
        public BaseEntityService(TUnitOfWork unitOfWork, IBaseBLLMapper<TDALEntity, TBLLEntity> mapper,
            TServiceRepository serviceRepository)
        {
            ServiceUnitOfWork = unitOfWork;
            ServiceRepository = serviceRepository;
            Mapper = mapper;
        }


        public async Task<IEnumerable<TBLLEntity>> AddRangeAsync(IEnumerable<TBLLEntity> entities)
        {
            return (await ServiceRepository.AddRangeAsync(entities.Select(b=>Mapper.Map<TBLLEntity, TDALEntity>(b))))
                .Select(e => Mapper.Map<TDALEntity, TBLLEntity>(e));
        }

        public virtual async Task<IEnumerable<TBLLEntity>> AllAsync(object? userId = null) =>
            (await ServiceRepository.AllAsync(userId)).Select(entity => Mapper.Map<TDALEntity, TBLLEntity>(entity));

        
        public virtual async Task<TBLLEntity> FirstOrDefaultAsync(TKey id, object? userId = null)
        {
            var dalEntity = await ServiceRepository.FirstOrDefaultAsync(id, userId);
            var result = Mapper.Map(dalEntity);
            return result;
        }
        
        public virtual TBLLEntity Add(TBLLEntity entity)
        {
            var dalEntity = Mapper.Map<TBLLEntity, TDALEntity>(entity);
            var trackedEntity = ServiceRepository.Add(dalEntity);
            ServiceUnitOfWork.AddToEntityTracker(trackedEntity, entity);
            var res  = Mapper.Map<TDALEntity, TBLLEntity>(trackedEntity);
            return res;
        }

        public virtual async Task<TBLLEntity>UpdateAsync(TBLLEntity entity, object? userId = null) =>
            Mapper.Map<TDALEntity, TBLLEntity>(await ServiceRepository.UpdateAsync(Mapper.Map<TBLLEntity, TDALEntity>(entity),userId));
        

        public virtual async Task<TBLLEntity> RemoveAsync(TBLLEntity entity, object? userId = null) =>
            Mapper.Map<TDALEntity, TBLLEntity>(await ServiceRepository.RemoveAsync(Mapper.Map<TBLLEntity, TDALEntity>(entity)));


        public virtual async Task <TBLLEntity> RemoveAsync(TKey id, object? userId = null) =>
            Mapper.Map<TDALEntity, TBLLEntity>(await ServiceRepository.RemoveAsync(id));
        
        public virtual async Task <bool> ExistsAsync(TKey id, object? userId = null) =>
            await ServiceRepository.ExistsAsync(id, userId);
        
    }
}