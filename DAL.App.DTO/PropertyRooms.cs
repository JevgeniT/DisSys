using System;
using Contracts.DAL.Base;
using DAL.Base;
 
namespace DAL.App.DTO
{
    
    
    public class PropertyRooms : PropertyRooms<Guid>, IDomainBaseEntity
    {
    }

    public class PropertyRooms<TKey>: IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public TKey AppUserId { get; set; }= default!;
 
        public TKey PropertyId { get; set; } = default!;
        public Property? Property { get; set; }

        public TKey RoomId { get; set; }= default!;
        public Room? Room { get; set; }

        public int Quantity { get; set; }
        
        public DateTime AvailableFrom { get; set; }
        
        public DateTime AvailableTo { get; set; }
        
        
    }
}