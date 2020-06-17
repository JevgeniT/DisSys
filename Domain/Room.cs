using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace Domain
{
    public class Room : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }
        public string? RoomName { get; set; }
        public int RoomCapacity { get; set; }
        public int RoomSize { get; set; }  //m2
        public string Description { get; set; }
        
        public Guid PropertyId { get; set; }
        
        public Property? Property { get; set; }
        
        public ICollection<Availability>? Availabilities { get; set; }
        public enum BedType { Large, Single, Double }

        

    }
}