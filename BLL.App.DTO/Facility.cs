using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Facility : Facility<Guid>, IDomainBaseEntity
    {
    }
    public class Facility<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public  string? Name { get; set; }

        public TKey Id { get; set; } = default!;
    }
}