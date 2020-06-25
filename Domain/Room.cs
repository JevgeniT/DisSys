using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace Domain
{
    public class Room : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public int Size { get; set; }  //m2
        public string Description { get; set; }
        
        public Guid PropertyId { get; set; }
        public BedType Bed { get; set; }
        public Property? Property { get; set; }
        
        public ICollection<Availability>? Availabilities { get; set; }

        public ICollection<Facility>? RoomFacilities { get; set; }

    }
        public enum BedType { Large, Single, Double }
}