using System;
using System.Collections.Generic;

namespace Public.DTO
{
    public class LocationDTO
    {
        public int Guid { get; set; }

        public string Country { get; set; }
        
        public string City { get; set; }

        public IEnumerable<PropertyDTO>? LocationProperties { get; set; }
             
    }
}