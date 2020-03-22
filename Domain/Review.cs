using DAL.Base;

namespace Domain
{
    public class Review : DomainEntity
    {

        public Reservation Reservation { get; set; }

        public int Score { get; set; }
        
        
        
    }
}