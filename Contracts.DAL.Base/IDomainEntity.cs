using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntity : IDomainEntity<int>
    {
        public int Id { get; set; }
    }
    
    
    public interface IDomainEntity<TKey> : IDomainBaseEntity<TKey>, IDomainEntityMetadata
            //where TKey : struct, IComparable
        where TKey : struct, IEquatable<TKey> { }

   
    
}