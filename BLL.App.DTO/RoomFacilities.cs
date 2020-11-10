using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    
    public class RoomFacilities : RoomFacilities<Guid>, IDomainBaseEntity
    {
    }
    
    public class RoomFacilities<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public Guid RoomId { get; set; }

        public Room? Room { get; set; }
        
        public Guid FacilityId { get; set; }

        public Facility? Facility { get; set; }
    }
}