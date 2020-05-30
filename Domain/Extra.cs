using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Extra : IDomainEntityBaseMetadata
    {
 
        public string ExtraName { get; set; }

        public Guid FacilityId { get; set; }
        public Facility? Facility { get; set; }


        public Guid Id { get; set; }
    }
}