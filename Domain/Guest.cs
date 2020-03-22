using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Guest: DomainEntity 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [InverseProperty(nameof(Reservation.ReservedBy))]
        
        public ICollection<Reservation>? GuestReservations { get; set; }
    }
}
