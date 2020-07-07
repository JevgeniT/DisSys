using System;

namespace Public.DTO
{
    public class SearchDTO
    {
        public string? Input { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        
        public int Adults { get; set; }
       
        public int Childrens { get; set; }
        public Guid PropertyId { get; set; }
    }
}