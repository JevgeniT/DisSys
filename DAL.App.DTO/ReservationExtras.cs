using System;
using Contracts.DAL.Base;
using DAL.Base;

namespace DAL.App.DTO
{
    public class ReservationExtras : ReservationExtras<Guid>, IDomainEntityBaseMetadata
    {
    }
    public class ReservationExtras<TKey> : DomainEntityBaseMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey ReservationId { get; set; }  = default!;
        public Reservation? Reservation { get; set; }
        public TKey ExtraId { get; set; } = default!;
        public Extra? Extra { get; set; }
    }
}