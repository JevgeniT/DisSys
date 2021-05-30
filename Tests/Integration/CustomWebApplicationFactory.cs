using System;
using System.Collections.Generic;
using System.Linq;
using DAL.App.EF;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the dbcontext
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                    );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<AppDbContext>(options =>
                {
                    // do we need unique db?
                    options.UseInMemoryDatabase("TestDB");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                SeedData(db);
                // seed data
               
            });
        }


        public void SeedData(AppDbContext context)
        {
            var id = SeedIdentity(context);
            try
            {
                SeedProperty(context, id);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception ");
            }

            // if (context.Rooms.Any())
            // {
            //     var id2 = context.Rooms.FirstOrDefault()?.Id;
            //     var id3 = context.Rooms.FirstOrDefault()?.PropertyId;
            //
            //     throw new Exception(id2.ToString() + " "+ id3.ToString());
            // }
        }

        public static Guid? SeedIdentity(AppDbContext context)
        {
            
            Guid hostRoleId = Guid.NewGuid();
            Guid guestRoleId = Guid.NewGuid();

            Guid hostId = Guid.NewGuid();
            Guid guestId = Guid.NewGuid();

            var hasher = new PasswordHasher<AppUser>();
            if (context.Users.Any())
            {
                return context.Users?.FirstOrDefault(u=> u!.Email == "host@host.com")!.Id;
            }
            context.Roles.AddRange(
                    new AppRole() 
                {
                    Name = "host",
                    NormalizedName = "Host",
                    Id = hostRoleId
                },
                    new AppRole()
                {
                    Name = "gust",
                    NormalizedName = "Guest",
                    Id = guestRoleId,

                });
            
            context.Users.AddRange(
               new AppUser()
               {
                   Id = guestId,
                   Email = "user@user.com",
                   UserName = "user@user.com",
                   NormalizedEmail = "user@user.com".ToUpper(),
                   NormalizedUserName = "user@user.com".ToUpper(),
                   FirstName = "user",
                   LastName = "user",
                   PasswordHash = hasher.HashPassword(null!, "qweqwe"!),
                   SecurityStamp = String.Empty,
                   EmailConfirmed = true,
                   LockoutEnd = DateTimeOffset.MaxValue,
                   PhoneNumber = ""
               },
               new AppUser()
               {
                   Id = hostId,
                   Email = "host@host.com",
                   UserName = "host@host.com",
                   FirstName = "host",
                   LastName = "host",
                   NormalizedEmail = "host@host.com".ToUpper(),
                   NormalizedUserName = "host@host.com".ToUpper(),
                   SecurityStamp = String.Empty,
                   PasswordHash = hasher.HashPassword(null!, "qweqwe"!),
                   EmailConfirmed = true,
                   LockoutEnd = DateTimeOffset.MaxValue,
                   PhoneNumber = ""
               });

           context.Add<IdentityUserRole<Guid>>(
               new IdentityUserRole<Guid>
           {
               RoleId = hostRoleId,
               UserId = hostId
           });

           context.Add<IdentityUserRole<Guid>>(
               new IdentityUserRole<Guid>
               {
                   RoleId = guestRoleId,
                   UserId = guestId
               });
           context.SaveChanges();

           return hostId;
        }

        public static void SeedProperty(AppDbContext context, Guid? hostId)
        {
            
            context.Properties.Add(new Property
            {
                Name = "ForRooms",
                Address = "Test",
                Description = "Test",
                Country = "Test",
                Type = "Hotel",
                Reviews = null,
                PropertyRooms = new List<Room>()
                {
                    new Room
                    {
                        Name = "room",
                        AdultsOccupancy = 10,
                        ChildOccupancy = 10,
                        Size = 1,
                        Description = "null",
                        AllowSmoking = false,
                        RoomAvailabilities = new List<Availability>()
                        {
                            new Availability
                            {
                                From = DateTime.Now,
                                To =   DateTime.Now.AddDays(10),
                                Active = true,
                                PricePerNightForAdult = 10,
                                PricePerNightForChild = 10,
                                PricePerPerson = false,
                                RoomsAvailable = 10
                            }
                        },
                        BedTypes = new List<string>(){""},
                        RoomFacilities = null
                    }
                },
                Extras = null,
                PropertyRules = null,
                AppUserId = hostId!.Value,
            });
            context.SaveChanges();
        }
    }
}