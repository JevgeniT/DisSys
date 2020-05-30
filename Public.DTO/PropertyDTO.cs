using System;

namespace Public.DTO
{
    public class PropertyDTO
    {
        
        public Guid Guid { get; set; }
        public string PropertyName { get; set; }
  
        public string Address { get; set; }

        public int LocationDTOId { get; set; }
        
    }
}