using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork: IBaseUnitOfWork, IBaseEntityTracker
    {
        IPropertyRepository Properties { get; }
        IRoomRepository Rooms { get; }
        IAvailabilityRepository Availabilities { get; }
        IMongoAvailabilityRepository MongoAvailabilities { get; }
        IAvailabilityPoliciesRepository AvailabilityPolicies { get; }
        IReservationRoomsRepository ReservationRooms { get; }
        IRoomFacilitiesRepository RoomFacilities { get; }

        IExtraRepository Extras { get; }
        IFacilityRepository Facilities { get; }
        IReviewRepository Reviews { get; }
        IInvoiceRepository Invoices { get; }
        IReservationRepository Reservations { get; }
        IPolicyRepository Policies { get; }

    }
}
