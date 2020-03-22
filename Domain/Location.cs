using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Location : DomainEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        
        [InverseProperty(nameof(Property.PropertyLocation))]
        public ICollection<Property>? LocationProperties { get; set; }    
        
    }
}
