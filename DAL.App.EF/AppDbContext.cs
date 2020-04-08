using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: DbContext
    {
        //
        // public DbSet<Extra> Extras { get; set; }
        // public DbSet<Facility> Facilities { get; set; }
        public DbSet<Location> Locations { get; set; }
        // public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        // public DbSet<Review> Reviews { get; set; }
        public DbSet<Guest> Guests { get; set; }
        // public DbSet<Policy> Policies { get; set; }
        public DbSet<Property> Properties { get; set; }
        // public DbSet<PropertyRooms> PropertyRooms { get; set; }
        // public DbSet<Price> Price { get; set; }
        // public DbSet<GuestReservations> GuestReservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        // public DbSet<RoomFacilities> RoomFacilities { get; set; }
        // public DbSet<RoomPolicies> RoomPolicies { get; set; }

        
   //15 entities     
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            foreach (var relationship in builder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

    }
}