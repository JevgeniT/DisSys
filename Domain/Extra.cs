using System;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Extra : Extra<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class Extra<TKey> : DomainEntityBaseMetadata<TKey>
    where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public string? Name { get; set; }
        public TKey PropertyId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
    
}