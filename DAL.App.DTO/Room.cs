using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace DAL.App.DTO
{

    public class Room : Room<Guid>, IDomainBaseEntity
    {
        
    }
    
    public class Room <TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public string? Name { get; set; }
        public int AdultsOccupancy { get; set; }
        public int ChildOccupancy { get; set; }
        public int Size { get; set; }  //m2
        public string? Description { get; set; }
        public TKey PropertyId { get; set; }= default!;
        public Property? Property { get; set; }
        public ICollection<string>? BedTypes { get; set; }
        public ICollection<Availability>? RoomAvailabilities { get; set; }
        public ICollection<Facility>? RoomFacilities { get; set; }
    }
    
}