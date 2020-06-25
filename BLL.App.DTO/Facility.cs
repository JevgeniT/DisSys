using System;
using System.Text.Json.Serialization;
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

        [JsonIgnore]
        public Guid RoomId { get; set; }
        [JsonIgnore]

        public TKey Id { get; set; } = default!;
    }
}