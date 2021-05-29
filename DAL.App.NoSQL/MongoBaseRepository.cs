using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Mappers;
using Contracts.DAL.Base.Repositories;

namespace DAL.App.NoSQL
{
    public  class MongoBaseRepository<TDALEntity> 
        : MongoBaseRepository<Guid,TDALEntity> ,IBaseRepository<TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<Guid>, new()
     {
        public MongoBaseRepository() : base()
        {
            
        }
    }
    
    public  class MongoBaseRepository<TKey,TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        public TDALEntity Add(TDALEntity entity, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TDALEntity>> AddRangeAsync(IEnumerable<TDALEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TDALEntity>> AllAsync(object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<TDALEntity> FirstOrDefaultAsync(TKey id, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<TDALEntity> UpdateAsync(TDALEntity entity, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<TDALEntity> RemoveAsync(TDALEntity entity, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<TDALEntity> RemoveAsync(TKey id, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}