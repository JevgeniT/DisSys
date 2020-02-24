using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Extra> Extras { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<GuestReservations> GuestReservations { get; set; }
        public DbSet<RoomFacilities> RoomFacilities { get; set; }
        public DbSet<RoomPolicies> RoomPolicies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}