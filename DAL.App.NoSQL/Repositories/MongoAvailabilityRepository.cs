using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF;
using MongoDB.Driver;

namespace DAL.App.NoSQL.Repositories
{
    public class MongoAvailabilityRepository : IMongoAvailabilityRepository
    {
        private readonly MongoContext _context;

        private readonly DALMapper<Domain.Availability, Availability> _mapper =
            new DALMapper<Domain.Availability, Availability>();
        
        public MongoAvailabilityRepository(MongoContext context)
        {
            _context = context;
        }
        

        public async Task CreateNew(Guid propertyId)
        {
            
            // Availability entity = new Availability(){Id = propertyId,Months = new List<Month>()};
            //
            // for (int i = DateTime.Now.Month; i <=12; i++)
            // {
            //     Month month = new Month()
            //     {
            //         Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
            //         Days = new List<Day>()
            //     };
            //
            //     for (int d = 1; d <= DateTime.DaysInMonth(DateTime.Now.Year,i); d++)
            //     {
            //         month.Days.Add(new Day{Name = $"{d}"});
            //     }
            //     entity.Months.Add(month);
            // }
            // await _context.MongoAvailabilities.InsertOneAsync(_mapper.Map(entity));
        }

        public Availability Add(Availability entity)
        {
            throw new NotImplementedException();

        }

        public Task<IEnumerable<Availability>> AddRangeAsync(IEnumerable<Availability> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Availability>> AllAsync(object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Availability> FirstOrDefaultAsync(Guid id, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Availability> UpdateAsync(Availability entity, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Availability> RemoveAsync(Availability entity, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Availability> RemoveAsync(Guid id, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id, object? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}