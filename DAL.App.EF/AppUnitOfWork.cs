using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.App.NoSQL;
using DAL.App.NoSQL.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        public IPropertyRepository Properties => GetRepository<IPropertyRepository>(() => new PropertyRepository(UOWDbContext));
        public IRoomFacilitiesRepository RoomFacilities => GetRepository<IRoomFacilitiesRepository>(() => new RoomFacilitiesRepository(UOWDbContext));

        public IRoomRepository Rooms => GetRepository<IRoomRepository>(() => new RoomRepository(UOWDbContext));
        public IAvailabilityRepository Availabilities => GetRepository<IAvailabilityRepository>(() => new AvailabilityRepository(UOWDbContext));
        public IMongoAvailabilityRepository MongoAvailabilities => GetRepository<IMongoAvailabilityRepository>(() => new MongoAvailabilityRepository(new MongoContext(new MongoConnectionSettings())));
        public IAvailabilityPoliciesRepository AvailabilityPolicies => GetRepository<IAvailabilityPoliciesRepository>(() => new AvailabilityPoliciesRepository(UOWDbContext));
        public IReservationRoomsRepository ReservationRooms => GetRepository<IReservationRoomsRepository>(() => new ReservationRoomsRepository(UOWDbContext));
        public IExtraRepository Extras => GetRepository<IExtraRepository>(() => new ExtraRepository(UOWDbContext));
        public IFacilityRepository Facilities => GetRepository<IFacilityRepository>(() => new FacilityRepository(UOWDbContext));
        public IReviewRepository Reviews => GetRepository<IReviewRepository>(() => new ReviewRepository(UOWDbContext));
        public IInvoiceRepository Invoices => GetRepository<IInvoiceRepository>(() => new InvoiceRepository(UOWDbContext));
        public IReservationRepository Reservations => GetRepository<IReservationRepository>(() => new ReservationRepository(UOWDbContext));
        public IPolicyRepository Policies => GetRepository<IPolicyRepository>(() => new PolicyRepository(UOWDbContext));

    } 
}
