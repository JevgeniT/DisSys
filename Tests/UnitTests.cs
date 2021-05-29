using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using DAL.App.EF;
using DAL.Base.EF.Repositories;
using Domain;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Availability = Domain.Availability;

namespace Tests
{
    public class Tests
    {
        private IAvailabilityService? _service;
        private AppDbContext? _context;
        private IAppUnitOfWork? uow;

        private static readonly Guid Id = Guid.Parse("43952f24-8822-4cf6-a4d9-df02acc483a9");
        private static readonly Guid propertyId = Guid.Parse("ef22ff03-c6db-4913-b7b1-28d3ac17eeb1");
        private static readonly Guid roomId = Guid.Parse("3aeaab72-fa4a-484a-bfaa-ede04f406460");

        [SetUp]
        public void Setup()
        {
            var provider = Substitute.For<IUserNameProvider>();
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
      
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new AppDbContext(optionBuilder.Options , provider); 
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
               
            using (var context = new AppDbContext(optionBuilder.Options, provider))
            {
                var date1 = new Domain.Availability
                {
                    Id = Id,
                    Active = true,
                    PricePerNightForAdult = 0,
                    PricePerNightForChild = 0,
                    PricePerPerson = false,
                    RoomsAvailable = 1,
                    RoomId = roomId,
                    From = DateTime.Now,
                    To = DateTime.Now.AddDays(5)
                };
                var date2 = new Domain.Availability
                {
                    Active = true,
                    PricePerNightForAdult = 0,
                    PricePerNightForChild = 0,
                    RoomId = roomId,
                    PricePerPerson = false,
                    RoomsAvailable = 1,
                    From = DateTime.Now.AddDays(4),
                    To = DateTime.Now.AddDays(15)
                };
                context.Availabilities.AddRange(new List<Availability>(){date1, date2});
                context.SaveChanges();

                var room = new Domain.Room
                {
                    Id = roomId,
                    PropertyId = propertyId
                };
                context.Rooms.Add(room);
                context.SaveChanges();

            };
            
            uow = Substitute.For<AppUnitOfWork>(_context);
            _service = new AvailabilityService(uow);
        }
        
        [Test]
        public void CanAdd()
        {
            var date = new BLL.App.DTO.Availability { };
            var entity = _service?.Add(date);
            Assert.NotNull(entity);
            Assert.NotNull(entity?.Id);
            Assert.That(entity, Is.TypeOf<BLL.App.DTO.Availability>());
        }
        
        [Test]
        public async Task CanUpdate()
        {
            var date = new BLL.App.DTO.Availability
            {
                Id = Id,
                Active = true,
                PricePerNightForAdult = 10,
                PricePerNightForChild = 10,
                PricePerPerson = false,
                RoomsAvailable = 0
            };
            
            var entity = await _service!.UpdateAsync(date);
            Assert.True(entity.PricePerNightForAdult == 10);
            Assert.True(entity.PricePerNightForChild == 10);
            Assert.NotNull(entity?.Id);
        }
        
        [Test]
        public Task UpdateNonExistentThrowsException()
        {
            var date = new BLL.App.DTO.Availability
            {
                Id = Guid.NewGuid(),
            };
           Assert.ThrowsAsync<LogicException>(async () => await _service!.UpdateAsync(date));
          
           return Task.CompletedTask;
        }
        
        [Test]
        public async Task CanRemove()
        {
            var date = new BLL.App.DTO.Availability
            {
                Id = Id,
            };
            var removed = await _service!.RemoveAsync(date);
            
            Assert.AreEqual(Id, removed.Id);
        }
        
        [Test]
        public Task RemoveNonExistentThrowsException()
        {
            Assert.ThrowsAsync<LogicException>(async () => await _service!.RemoveAsync(Guid.NewGuid()));
            return Task.CompletedTask;
        }
        
        [Test]
        public async Task CanFindOneAvailableDate()
        {
           var dates =  
               await _service!.FindAvailableDates(DateTime.Now, DateTime.Now.AddDays(1), propertyId);
           Assert.NotNull(dates);
           Assert.True(dates.Count() == 1);
        }
        
        [Test]
        public async Task CanFindMultipleAvailableDates()
        {
            var dates =  
                await _service!.FindAvailableDates(DateTime.Now, DateTime.Now.AddDays(10), propertyId);
            Assert.True(dates.Count() == 2);
        }
        
        [Test]
        public async Task CannotFindAnyDatesReturnsFalse()
        {
            var dates = await _service!.ExistsAsync(default, default, new List<Guid>() {roomId});
            Assert.IsFalse(dates);
        }
    }
}