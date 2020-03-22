using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Reservation : DomainEntity
    {

        public int ReservationNumber { get; set; }

        public DateTime MadeAt { get; set; } = DateTime.Now;
        //approx
        public DateTime CheckIn { get; set; }

        public DateTime CheckInDate{ get; set; }
       
        public DateTime CheckOutDate { get; set; }
        
        // public ICollection<Room> Rooms { get; set; }

        public int PropertyId { get; set; }        
        
        // public Property Property { get; set; }

        // public Extra Extra { get; set; }    

        [ForeignKey(nameof(ReservedBy))]
        public int GuestReservationsId { get; set; }
        public Guest? ReservedBy { get; set; }
    }
}