using DAL.Base;

namespace Domain
{
    public class GuestReservations : DomainEntity
    {

         public Guest Guest { get; set; }
        
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        
    }
}