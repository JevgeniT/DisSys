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
    public class AvailabilityService : 
        BaseEntityService<IAvailabilityRepository, IAppUnitOfWork, DAL.App.DTO.Availability, Availability>, IAvailabilityService
    {
        public AvailabilityService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Availability, Availability>(), unitOfWork.Availabilities)
        {
        }

        public async  Task<IEnumerable<Availability>> AllAsync(Guid? roomId = null)
        {
             return (await ServiceRepository.AllAsync( roomId)).Select( dalEntity => Mapper.Map(dalEntity) );
        }
        
        public async Task<IEnumerable<Availability>> FindAvailableDates(DateTime @from, DateTime to, Guid propertyId)
        {
            return (await ServiceRepository.FindAvailableDates(from, to, propertyId)).Select( dalEntity => Mapper.Map(dalEntity) );
        }
        
        public async Task<bool> ExistsAsync(Availability availability)
        {
            return  await ServiceRepository.ExistsAsync(Mapper.Map<Availability,DAL.App.DTO.Availability>(availability));
        }
        
        
        public async Task<bool> ExistsAsync(DateTime from, DateTime to, List<Guid> roomIds)
        {
            return  await ServiceRepository.ExistsAsync(from, to, roomIds);
        }
        
        
        public async Task SaveOnChangeAsync(DateTime @from, DateTime to, Guid propertyId, List<Guid>? roomIds) // TODO
        {
            var list = ServiceRepository.FindAvailableDates(from, to, propertyId).Result.ToList();
            
            foreach (var available in list.Where(available => roomIds.Contains(available.RoomId)))
            {
                available.Room = null;
                if (available.RoomsAvailable>1)
                {
                    available.RoomsAvailable -= 1;
                    await UpdateAsync(Mapper.Map(available));
                    continue;
                }
                if ((available.From == @from && available.To > to) || (available.To == to && available.From<@from))
                {
                    available.From = @from == available.From ? to : available.From;
                    available.To = to == available.To ? @from : available.To;
                }
                else if (available.From< @from && available.To > to)  
                {
                    var first = available.DeepCopy();
                    first.Id = Guid.NewGuid();
                    first.To = from;
                    Add(Mapper.Map(first));

                    var second = available.DeepCopy();
                    second.From = to;
                    second.Id = Guid.NewGuid();
                    Add(Mapper.Map(second));
                    available.Active = false;
                }
                else
                {
                    available.Active = false;
                }

                await UpdateAsync(Mapper.Map(available));
            }
        }
    }
}