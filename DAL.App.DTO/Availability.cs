using System;
  using Contracts.DAL.Base;
 
namespace DAL.App.DTO
{
    public class Availability : Availability<Guid>, IDomainBaseEntity
    {
    }

    public class Availability<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public DateTime start { get; set; }

 
        public DateTime end { get; set; }


        public Guid? RoomId { get; set; }

    }
}