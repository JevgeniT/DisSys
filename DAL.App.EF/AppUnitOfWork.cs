using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        
        public IPropertyRepository Properties =>
            GetRepository<IPropertyRepository>(() => new PropertyRepository(UOWDbContext));
        public IRoomRepository Rooms =>
             GetRepository<IRoomRepository>(() => new RoomRepository(UOWDbContext));

        public IAvailabilityRepository Availabilities =>
            GetRepository<IAvailabilityRepository>(() => new AvailabilityRepository(UOWDbContext));

        public IExtraRepository Extras => GetRepository<IExtraRepository>(() => new ExtraRepository(UOWDbContext));
        public IFacilityRepository Facilities => GetRepository<IFacilityRepository>(() => new FacilityRepository(UOWDbContext));
         public IReviewRepository Reviews => GetRepository<IReviewRepository>(() => new ReviewRepository(UOWDbContext));
        public IInvoiceRepository Invoices => GetRepository<IInvoiceRepository>(() => new InvoiceRepository(UOWDbContext));
        public IReservationRepository Reservations => GetRepository<IReservationRepository>(() => new ReservationRepository(UOWDbContext));
        public IPolicyRepository Policies => GetRepository<IPolicyRepository>(() => new PolicyRepository(UOWDbContext));

        public IPropertyRoomsRepository PropertyRooms => GetRepository<IPropertyRoomsRepository>(() => new PropertyRoomsRepository(UOWDbContext));
        public IRoomFacilitiesRepository RoomFacilities => GetRepository<IRoomFacilitiesRepository>(() => new RoomFacilitiesRepository(UOWDbContext));
    } 
}
