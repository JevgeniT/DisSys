using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    
    
    public class PropertyRooms : PropertyRooms<Guid, AppUser>, IDomainEntityUser<AppUser>
    {
        
    }

    public class PropertyRooms<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey AppUserId { get; set; }= default!;
        public TUser? AppUser { get; set; }

        public TKey PropertyId { get; set; } = default!;
        public Property? Property { get; set; }

        public TKey RoomId { get; set; }= default!;
        public Room? Room { get; set; }

        public int Quantity { get; set; }
        
        public DateTime AvailableFrom { get; set; }
        
        public DateTime AvailableTo { get; set; }
        
        
    }
}