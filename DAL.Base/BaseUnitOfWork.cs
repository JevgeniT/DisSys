using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class BaseUnitOfWork<TKey> : IBaseUnitOfWork, IBaseEntityTracker<TKey> 
        where TKey : IEquatable<TKey>
    {
        private readonly Dictionary<Type, object> _repoCache = new();

        private readonly Dictionary<IDomainBaseEntity<TKey>, IDomainBaseEntity<TKey>> _entityTracker = new();
        
        public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
        {
            if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository) repo;
            }

            repo = repoCreationMethod()!;
            _repoCache.Add(typeof(TRepository), repo);
            return (TRepository) repo;
        }
        public abstract int SaveChanges();

        public abstract Task<int> SaveChangesAsync();
        
      

        public void AddToEntityTracker(IDomainBaseEntity<TKey> internalEntity, IDomainBaseEntity<TKey> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }
        
        protected void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }
    }
}