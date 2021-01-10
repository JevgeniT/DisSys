using System;

namespace Public.DTO.Reservation
{
    public class ReservationPreviewDTO  
    {
        public Guid Id { get; set; }
        public int ReservationNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool Active { get; set; }
        public decimal TotalPrice { get; set; }
        public string? PropertyName { get; set; }
        public string? Status { get; set; }
        public string? ReservedBy { get; set; }
    }
}