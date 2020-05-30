using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Room : IDomainEntityBaseMetadata
    {

        public string RoomName { get; set; }
        public int RoomCapacity { get; set; }
        public int RoomSize { get; set; }  //m2

        [ForeignKey(nameof(RoomPropertyId))]
        public Guid RoomPropertyId { get; set; }
        
        public Property? RoomProperty { get; set; }
        
        public ICollection<RoomAvailability>? RoomAvailabilities { get; set; }
        
        public enum BedType
        {
            Large, Single, Double 
        }

        public Guid Id { get; set; }
    }
}