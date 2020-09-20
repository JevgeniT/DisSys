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
        
        public async Task<bool> ExistsAsync(DateTime from, DateTime to)
        {
            return  await ServiceRepository.ExistsAsync(from, to);
        }
        
        
        public async Task<bool> ExistsAsync(DateTime from, DateTime to, Guid propertyId)
        {
            return  await ServiceRepository.ExistsAsync(from, to, propertyId);
        }
        
        public async Task ParseDate(List<Availability> list, DateTime @from, DateTime to) // TODO
        {
            foreach (var available in list)
            {
                if ((available.From == @from && available.To > to) || (available.To == to && available.From>@from))
                {
                    Availability availability = new Availability
                    {
                        From = @from == available.From ? to : available.From,
                        To = to == available.To ? @from : available.To,
                        PricePerNightForAdult = available.PricePerNightForAdult,
                        Active = true,
                        RoomId = available.RoomId,
                    };
                            
                        Add(availability);
                } 
                
                else if (available.From< @from && available.To > to)  
                {
                    Add(new Availability{ 
                            From = available.From, To = @from,
                            Active = true, 
                            PricePerNightForAdult = available.PricePerNightForAdult,
                            RoomId = available.RoomId,
                    });
                    Add(new Availability
                        {
                            From = available.To, To = available.To,
                            Active = true, 
                            PricePerNightForAdult = available.PricePerNightForAdult ,
                            RoomId = available.RoomId,
                        });
                }
                else if (available.From == @from && available.To == to)
                {
                    available.Active = false; 
                    await  UpdateAsync(available);
                    
                    
                }
            }

            
        }
        
        
    }
}