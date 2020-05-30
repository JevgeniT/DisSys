using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork: IBaseUnitOfWork
    {
         IPropertyRepository Properties { get; }
        IRoomRepository Rooms { get; }
 
        IExtraRepository Extras { get; }

        IFacilityRepository Facilities { get; }
        IPriceRepository Prices { get; }
        IReviewRepository Reviews { get; }
        IInvoiceRepository Invoices { get; }
        IReservationRepository Reservations { get; }

        IPolicyRepository Policies { get; }





        
    }
}
