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
    public class Room<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public string? Name { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildCapacity { get; set; }
        public int Size { get; set; }  //m2
        public string? Description { get; set; }
        public Guid PropertyId { get; set; }
        public Property? Property { get; set; }
        public BedType Bed { get; set; }
        public ICollection<Availability>? RoomAvailabilities { get; set; }
        public ICollection<Facility>? RoomFacilities { get; set; }
    }
        public enum BedType
        {
            Large, Single, Double 
        }
}