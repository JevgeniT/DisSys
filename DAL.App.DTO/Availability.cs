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

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public Guid PolicyId { get; set; }
        public Policy? Policy { get; set; }

        public bool IsUsed { get; set; }
        
        public Guid? RoomId { get; set; }
        public Room? Room { get; set; }

        public decimal PricePerNightForAdult { get; set; }
        
        public decimal PricePerNightForChild { get; set; }

        public bool PricePerPerson { get; set; }
        public int RoomsAvailable { get; set; }

    }
}