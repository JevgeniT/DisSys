using System;
using System.Collections.Generic;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class BaseDateProvider
    {


        public static void SeedFacilities(ModelBuilder builder)
        {
            List<string> facilities = new List<string>()
            {
                "Upper floors accessible by elevator",
                "Linens",
                "Wardrobe or closet",
                "Minibar",
                "Air conditioning",
                "Safe",
                "Ironing facilities",
                "Iron",
                "Heating",
                "Coffee machine",
                "Electric kettle",
                "Sofa",
                "Desk",
                "Satellite channels",
                "Flat-screen TV",
                "Balcony",
                "Outdoor furniture",
                "Wake-up service",
                "Free Wi-Fi"
            };
            
            builder.Entity<Facility>( b=> facilities.ForEach(f=> b.HasData(new {Id = Guid.NewGuid(),Name = f, ChangedAt =DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = "migration", ChangedBy="migration"})));
        }
        public static void SeedIdentity(ModelBuilder builder)
        {
            Guid  hostRoleId =  Guid.NewGuid();
            Guid  guestRoleId = Guid.NewGuid();
            
            Guid  hostId =  Guid.NewGuid();
            Guid  guestId = Guid.NewGuid();
             
            var hasher = new PasswordHasher<AppUser>();

            builder.Entity<AppRole>(b =>
            {
                b.HasData(new AppRole
                {
                    Name = "host",
                    NormalizedName = "Host",
                    Id = hostRoleId
                });
                b.HasData(new AppRole
                {
                    Name = "gust",
                    NormalizedName = "Guest",
                    Id = guestRoleId,
                    
                });
                   
            });

            builder.Entity<AppUser>(b =>
            {
                b.HasData(new AppUser()
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
                });
                b.HasData(new AppUser()
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
            });
            
            builder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                RoleId = hostRoleId,
                UserId = hostId
            });
            new IdentityUserRole<Guid>
            {
                RoleId = guestRoleId,
                UserId = guestId
            };
        }

     

        
        
    }
}