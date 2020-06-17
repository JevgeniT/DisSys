
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReservationRepository : EFBaseRepository<AppDbContext,Reservation, DAL.App.DTO.Reservation>,  IReservationRepository
    {
        public ReservationRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Reservation, DAL.App.DTO.Reservation>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.Reservation>> AllAsync(Guid? userId = null)
        {
             
            return await base.AllAsync();
           
        }
        
        public async Task<DAL.App.DTO.Reservation> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.Id == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
        
        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.Id == userId);
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }
        
        public async Task<IEnumerable<Reservation>> DTOAllAsync(Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<Reservation> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
    }
}