using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace DAL.App.DTO
{
    public class RoomPolicies : RoomPolicies<Guid>, IDomainBaseEntity
    {
    }
    public class RoomPolicies<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public Guid RoomId { get; set; }
        public Room? Room { get; set; }

        public Guid PolicyId { get; set; }
        public Policy? Policy { get; set; }
     }
}