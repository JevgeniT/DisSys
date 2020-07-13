using System;

namespace Public.DTO
{
    public class FacilityDTO
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        
        public Guid RoomId { get; set; }

    }
}