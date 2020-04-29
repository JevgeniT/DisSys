using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntity : IDomainEntity<Guid>
    {
        public Guid Id { get; set; }
    }
    
    
    public interface IDomainEntity<TKey> : IDomainBaseEntity<TKey>, IDomainEntityMetadata
            //where TKey : struct, IComparable
        where TKey : struct, IEquatable<TKey> { }

   
    
}