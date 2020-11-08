using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace Public.DTO
{
    public class RoomDTO : IDomainBaseEntity
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string? Name { get; set; }
        public int Size { get; set; }
        public int AdultsOccupancy { get; set; }
        public int ChildOccupancy{ get; set; }
        public string? Description { get; set; }
        public ICollection<FacilityDTO>? FacilityDtos { get; set; }

        public ICollection<string>? Facilities { get; set; }

    }
}