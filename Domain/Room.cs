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
        public TKey Id { get; set; }
        public string? Name { get; set; }
        
        public int AdultsOccupancy { get; set; }
        
        public int ChildOccupancy { get; set; }
        
        public int Size { get; set; }  //m2
        
        public string Description { get; set; }

        public bool AllowSmoking { get; set; }
        public TKey PropertyId { get; set; }
        public BedType Bed { get; set; }
        
        public Property? Property { get; set; }
        
        public ICollection<RoomFacilities>? RoomFacilities { get; set; }
    }
        public enum BedType { Large, Single, Double }
}