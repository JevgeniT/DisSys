using System;
using Contracts.DAL.Base;
 
namespace BLL.App.DTO
{
    public class Extra : Extra<Guid>, IDomainBaseEntity
    {
    }
    
    public class Extra<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public TKey PropertyId { get; set; } = default!;
    }
}