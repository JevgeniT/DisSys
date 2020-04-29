using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Property: DomainEntity 
    {
        public string PropertyName { get; set; }
  
        public string Address { get; set; }

        
        [ForeignKey(nameof(PropertyLocation))]
        public Guid PropertyLocationId { get; set; }
        public Location? PropertyLocation { get; set; }

        
        [InverseProperty(nameof(Room.RoomProperty))]
        public ICollection<Room>? PropertyRooms { get; set; }
        
        
        
    }
}