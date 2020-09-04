using System;

namespace Contracts.DAL.Base
{
    public interface IBaseEntityTracker : IBaseEntityTracker<Guid>
    {
        
    }
    
    public interface IBaseEntityTracker<TKey>
        where TKey: IEquatable<TKey>
    {
        void AddToEntityTracker(IDomainBaseEntity<TKey> internalEntity, IDomainBaseEntity<TKey> externalEntity);
    }
}