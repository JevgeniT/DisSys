using System;
using Contracts.DAL.Base;
 
namespace BLL.App.DTO
{
    public class Extra : Extra<Guid>, IDomainBaseEntity
    {
    }
    
    public class Extra<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
    
        public string Name { get; set; }

        public Guid FacilityId { get; set; }
        
        public Facility? Facility { get; set; }


        public TKey Id { get; set; } = default!;
    }
}