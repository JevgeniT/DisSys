using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Facility : Facility<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class Facility<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string? Name { get; set; }
    }
    
    
    
}