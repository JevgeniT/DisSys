using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.NoSQL;

namespace DAL.App.EF.Repositories
{
    public class MongoAvailabilityRepository : IMongoAvailabilityRepository
    {
        private readonly MongoContext _context;
        
        private readonly DALMapper<Domain.Availability, Availability> _mapper = new DALMapper<Domain.Availability, Availability>();
        
        public MongoAvailabilityRepository(MongoContext context)
        {
            _context = context;
        }

        public Availability Add(Availability entity)
        {
            var dal = _mapper.Map(entity);
            _context.MongoAvailabilities.InsertOneAsync(dal);
            Console.WriteLine("mongo");
            return new Availability();
        }

        public Task<IEnumerable<Availability>> AddRange(IEnumerable<Availability> entities)
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


        public async Task CreateNew(Guid propertyId)
        {
            Domain.Availability newOne =   new Domain.Availability(){Id = Guid.NewGuid()};
            
            List<Domain.Month> months = new List<Domain.Month>();
            
            for (int i = DateTime.Now.Month; i <=12; i++)
            {
                Domain.Month month = new Domain.Month()
                {
                    Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                    Days = new List<Domain.Day>()
                };

                for (int d = 1; d <= DateTime.DaysInMonth(DateTime.Now.Year,i); d++)
                {
                    month.Days.Add(new Domain.Day{Name = $"{d}"});
                }

                months.Add(month);
            }
            newOne.Months = months;
            
            await _context.MongoAvailabilities.InsertOneAsync(newOne);
        }
    }
}