using System;
using Contracts.DAL.Base;
 
namespace DAL.App.DTO
{
    public class Extra : Extra<Guid>, IDomainBaseEntity
    {
    }
    
    public class Extra<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
    
        public string ExtraName { get; set; }

        public Guid FacilityId { get; set; }
        public Facility? Facility { get; set; }


        public TKey Id { get; set; } = default!;
    }
}