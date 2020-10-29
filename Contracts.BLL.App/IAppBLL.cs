using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IPropertyService Properties { get; }
        public IInvoiceService Invoices { get; }
        public IRoomService Rooms { get; }
        public IReservationService Reservations { get; }
        public IExtraService Extras { get; }
        public IFacilityService Facilities { get; }
        public IPolicyService Policies { get; }
        public IReviewService  Reviews{ get; }
        public IAvailabilityService Availabilities { get; }
        public IMongoAvailabilityService MongoAvailabilities { get; }
        public IAvailabilityPoliciesService AvailabilityPolicies { get; }
        public IRoomFacilitiesService RoomFacilities { get;  }
        public IReservationRoomsService ReservationRooms { get; }





    }
}