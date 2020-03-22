using System;

namespace Contracts.DAL.Base
{
    public interface IDomainBaseEntity : IDomainBaseEntity<int>
    {
    }

    public interface IDomainBaseEntity<TKey> 
        where TKey : struct, IComparable
    {
        public TKey Id { get; set; } 
    }
}