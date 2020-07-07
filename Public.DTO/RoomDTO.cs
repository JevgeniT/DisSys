using System;
using System.Collections.Generic;

namespace Public.DTO
{
    public class RoomDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Size { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildCapacity { get; set; }
        public string? Description { get; set; }
        public ICollection<FacilityDTO> RoomFacilities { get; set; }
    }
}