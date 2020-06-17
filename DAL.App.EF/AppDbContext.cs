﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid >
    {
        private IUserNameProvider _userNameProvider;


        //
        public DbSet<Extra> Extras { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Policy> Policies { get; set; }

        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomFacilities> RoomFacilities { get; set; }
        public DbSet<RoomPolicies> RoomPolicies { get; set; }
 

        
         public AppDbContext(DbContextOptions<AppDbContext>? options, IUserNameProvider? userNameProvider)
           : base(options)
        {
           _userNameProvider = userNameProvider;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           base.OnModelCreating(builder);
           builder.Entity<Reservation>()
               .Property(b => b.ReservationNumber)
               .ValueGeneratedOnAdd();

           builder.Entity<Property>().Property(property => property.Type).HasConversion(type => type.ToString(),
               type =>  (PropertyType)Enum.Parse(typeof(PropertyType),type));
           
           foreach (var relationship in builder.Model
               .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
           {
               relationship.DeleteBehavior = DeleteBehavior.Restrict;
           }
           
           
        }


    }
}