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
        
        public async Task<IEnumerable<Availability>> FindAvailableDates(DateTime @from, DateTime to, Guid? PropertyId = null)
        {
            return (await ServiceRepository.FindAvailableDates(from, to, PropertyId)).Select( dalEntity => Mapper.Map(dalEntity) );
        }
        
        public async Task<bool> ExistsAsync(DateTime from, DateTime to)
        {
            return  await ServiceRepository.ExistsAsync(from, to);
        }
        
        public async Task ParseDate(List<Availability> list, DateTime From, DateTime To) // TODO
        {
            foreach (var available in list)
            {
                if ((available.From == From && available.To > To) || (available.To == To && available.From>From))
                {
                    Availability availability = new Availability
                    {
                        From = From == available.From ? To : available.From,
                        To = To == available.To ? From : available.To,
                        PricePerNightForAdult = available.PricePerNightForAdult,
                        IsUsed = false,
                        RoomId = available.RoomId,
                        // PolicyId = available.PolicyId

                    };
                            
                        Add(availability);
                } 
                
                else if (available.From< From && available.To > To)  
                {
                    Add(new Availability{ 
                            From = available.From, To = From,
                            IsUsed = false, 
                            PricePerNightForAdult = available.PricePerNightForAdult,
                            RoomId = available.RoomId,
                            // PolicyId = available.PolicyId
                        
                        });
                    Add(new Availability
                        {
                            From = available.To, To = available.To,
                            IsUsed = false, 
                            PricePerNightForAdult = available.PricePerNightForAdult ,
                            RoomId = available.RoomId,
                            // PolicyId = available.PolicyId
                        });
                }
                else if (available.From == From && available.To == To)
                {
                    available.IsUsed = true;
                    Update(available);

                }
            }

            
        }
        
        
    }
}