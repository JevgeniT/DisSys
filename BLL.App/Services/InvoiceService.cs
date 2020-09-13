
using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class InvoiceService:
        BaseEntityService<IInvoiceRepository, IAppUnitOfWork, DAL.App.DTO.Invoice, Invoice>, IInvoiceService
    {
        public InvoiceService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Invoice, Invoice>(), unitOfWork.Invoices)
        {
        }

    }
}