using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace DAL.App.DTO
{
    public class Price : Price<Guid>, IDomainBaseEntity
    {
    }
    public class Price<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public decimal Total { get; set; }
        public TKey Id { get; set; } = default!;
    }
}