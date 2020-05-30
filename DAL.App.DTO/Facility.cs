using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Facility : Facility<Guid>, IDomainBaseEntity
    {
    }
    public class Facility<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public virtual string FacilityName { get; set; }

        public TKey Id { get; set; } = default!;
    }
}