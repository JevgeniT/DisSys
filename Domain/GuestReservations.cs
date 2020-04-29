using System;
using DAL.Base;

namespace Domain
{
    public class GuestReservations : DomainEntity
    {

         public Guest Guest { get; set; }
        
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        
    }
}