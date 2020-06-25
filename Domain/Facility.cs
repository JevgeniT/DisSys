using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Facility : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid RoomId { get; set; }
    }
    
    
    
}