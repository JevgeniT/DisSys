using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ReservationExtrasService : BaseEntityService<IReservationExtrasRepository, IAppUnitOfWork, DAL.App.DTO.ReservationExtras, ReservationExtras>, IReservationExtrasService
    {
        public ReservationExtrasService(IAppUnitOfWork unitOfWork) 
             : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.ReservationExtras, ReservationExtras>(), unitOfWork.ReservationExtras)
        {
        }
    }
}