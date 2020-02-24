using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int ReservationNumber { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public Property? Property { get; set; }

        public Extra Extra { get; set; }
        
    }
}