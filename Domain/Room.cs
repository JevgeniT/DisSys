using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Room : Room<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class Room<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public TKey PropertyId { get; set; } = default!;
        public string? Name { get; set; }
        public int AdultsOccupancy { get; set; }
        public int ChildOccupancy { get; set; }
        public int Size { get; set; }  //m2
        public string Description { get; set; } = default!;
        public bool AllowSmoking { get; set; }
        public ICollection<Availability> RoomAvailabilities { get; set; } = default!;
        public BedType Bed { get; set; }
        
        public Property? Property { get; set; }
        
        public ICollection<RoomFacilities>? RoomFacilities { get; set; }
    }
        public enum BedType { Large, Single, Double }
}