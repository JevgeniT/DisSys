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
        public TKey PropertyId { get; set; } = default!;
        public Property? Property { get; set; }
        public string? Name { get; set; }
        public int AdultsOccupancy { get; set; }
        public int ChildOccupancy { get; set; }
        public int Size { get; set; }  //m2
        public string Description { get; set; } = default!;
        public bool AllowSmoking { get; set; }
        public ICollection<Availability>? RoomAvailabilities { get; set; }
        public ICollection<string>? BedTypes { get; set; }
        public ICollection<RoomFacilities>? RoomFacilities { get; set; }
    }
 }