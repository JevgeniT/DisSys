using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntity : IDomainEntity<int>
    {
        public int Id { get; set; }
    }
    
    
    public interface IDomainEntity<Tkey> : IDomainBaseEntity<Tkey>, IDomainEntityMetadata
            where Tkey : struct, IComparable 
    {
    
    }

   
}