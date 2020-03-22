using DAL.Base;

namespace Domain
{
    public class Extra : DomainEntity
    {
 
        public string ExtraName { get; set; }

        public int FacilityId { get; set; }
        public Facility? Facility { get; set; }
        
        
    }
}