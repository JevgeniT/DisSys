using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace Domain
{
    public class Room : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildCapacity { get; set; }
        public int Size { get; set; }  //m2
        public string Description { get; set; }
        public Guid PropertyId { get; set; }
        public BedType Bed { get; set; }
        public Property? Property { get; set; }
        public ICollection<Availability>? RoomAvailabilities { get; set; }
        public ICollection<Facility>? RoomFacilities { get; set; }
    }
        public enum BedType { Large, Single, Double }
}