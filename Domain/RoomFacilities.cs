using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class RoomFacilities: RoomFacilities<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    
    public class RoomFacilities<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public TKey RoomId { get; set; }

        public Room? Room { get; set; }
        
        public TKey FacilityId { get; set; }

        public Facility? Facility { get; set; }
    }
}