using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
   
    public class Room : IDomainBaseEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int AdultsOccupancy { get; set; }
        public int ChildOccupancy { get; set; }
        public int Size { get; set; }  //m2
        public ICollection<Availability>? RoomAvailabilities { get; set; }
        public BedType Bed { get; set; }
        public string? Description { get; set; }
        public Guid PropertyId { get; set; }
        public ICollection<Facility>? RoomFacilities { get; set; }
        
        
    }
        public enum BedType
        {
            Large,
            Single,
            Double 
        }
}