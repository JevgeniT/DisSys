
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class RoomFacilitiesRepository : EFBaseRepository<AppDbContext,RoomFacilities,RoomFacilities>,  IRoomFacilitiesRepository
    {
        public RoomFacilitiesRepository(AppDbContext dbContext) : 
            base(dbContext, new BaseDALMapper<RoomFacilities, RoomFacilities>())
        {
        }

 

        public IEnumerable<RoomFacilities> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<RoomFacilities>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override RoomFacilities Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<RoomFacilities> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override RoomFacilities Add(RoomFacilities entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override RoomFacilities Update(RoomFacilities entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override RoomFacilities Remove(RoomFacilities entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override RoomFacilities Remove(params object[] id)
        {
            return RepoDbSet.Remove(Find(id)).Entity;
        }

      
    }
}