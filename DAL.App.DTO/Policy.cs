using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace DAL.App.DTO
{
    public class Policy : Policy<Guid>, IDomainBaseEntity
    {
    }
    public class Policy<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    
    {
        public TKey Id { get; set; } = default!;

        public Guid PropertyId { get; set; }
        
        public string? Name { get; set; }

        public int PrepaymentBefore { get; set; }

        public int CancellationBefore { get; set; }
        
    }
}