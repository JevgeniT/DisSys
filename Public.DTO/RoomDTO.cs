using System;
using System.Collections.Generic;

namespace Public.DTO
{
    public class RoomDTO
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }

        public string? Name { get; set; }
        public int Size { get; set; }
        public int AdultsOccupancy { get; set; }
        public int ChildOccupancy{ get; set; }
        public string? Description { get; set; }
        public ICollection<FacilityDTO>? RoomFacilities { get; set; }

    }
}