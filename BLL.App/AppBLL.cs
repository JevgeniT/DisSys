using BLL.App.DTO;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
 
  namespace BLL.App
{
    public class AppBLL:BaseBLL<IAppUnitOfWork>,IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public IPropertyService Properties => GetService<IPropertyService>(() => new PropertyService(UnitOfWork));
        
        public IExtraService Extras => GetService<IExtraService>(() => new ExtraService(UnitOfWork));

        public IInvoiceService Invoices => GetService<IInvoiceService>(() => new InvoiceService(UnitOfWork));
        public IRoomService Rooms => GetService<IRoomService>(() => new RoomService(UnitOfWork));
        public IReservationService Reservations => GetService<IReservationService>(() => new ReservationService(UnitOfWork));
        public IFacilityService Facilities => GetService<IFacilityService>(() => new FacilityService(UnitOfWork));
        public IPolicyService Policies => GetService<IPolicyService>(() => new PolicyService(UnitOfWork));
        public IReviewService Reviews => GetService<IReviewService>(() => new ReviewService(UnitOfWork));
        public IAvailabilityService Availabilities => GetService<IAvailabilityService>(() => new AvailabilityService(UnitOfWork));
        
        public IAvailabilityPoliciesService AvailabilityPolicies => GetService<IAvailabilityPoliciesService>(() => new AvailabilityPoliciesService(UnitOfWork));

        
        public IReservationRoomsService ReservationRooms => GetService<IReservationRoomsService>(() => new ReservationRoomsService(UnitOfWork));
    }
} 