using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class RoomFacilitiesRepository
        : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, RoomFacilities, DAL.App.DTO.RoomFacilities>, IRoomFacilitiesRepository
    {
        public RoomFacilitiesRepository(AppDbContext dbContext)
            : base(dbContext, new DALMapper<RoomFacilities, DAL.App.DTO.RoomFacilities>())
        {
        }
    }
}