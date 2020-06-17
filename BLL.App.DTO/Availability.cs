using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Availability:Availability<Guid>, IDomainBaseEntity
    {
    }
    
    public class Availability<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public DateTime From { get; set; }

        public DateTime To { get; set; }
        
        public Guid RoomId { get; set; }

        public bool IsUsed { get; set; }
        
        public decimal PricePerNight { get; set; }
    }
     
}