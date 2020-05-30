using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Facility : IDomainEntityBaseMetadata
    {

        public string FacilityName { get; set; }

        public Guid Id { get; set; }
    }
}