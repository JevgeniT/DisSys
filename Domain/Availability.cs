using System;
using Contracts.DAL.Base;


namespace Domain
{
 
    public class Availability : IDomainEntityBaseMetadata
    {
        public Guid Id { get; set; }

        public DateTime From { get; set; }
        
        public DateTime To { get; set; }
        
        public Guid PropertyRoomId { get; set; }

        public bool IsUsed { get; set; } = false;
        public decimal PricePerNight { get; set; }


        public override string ToString()
        {
            return $"From: {From}, To: {To}, IsUsed: {IsUsed}";
        }
    }
}