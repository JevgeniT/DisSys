using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ReservationService : 
        BaseEntityService<IReservationRepository, IAppUnitOfWork, DAL.App.DTO.Reservation, Reservation>, IReservationService
    {
        public ReservationService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Reservation, Reservation>(), unitOfWork.Reservations)
        {
        }
        
        public async Task<IEnumerable<Reservation>> AllAsync(Guid? userId = null, Guid? propertyId = null)
        {
            return (await ServiceRepository.AllAsync(userId, propertyId)).Select( dalEntity => Mapper.Map(dalEntity) );
        }

        public override async Task<Reservation> FirstOrDefaultAsync(Guid id, object? userId = null)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId));
        }
    }
}