using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Guest> Guest { get; set; }

        public DbSet<Domain.Extra> Extra { get; set; }

        public DbSet<Domain.Facility> Facility { get; set; }

        public DbSet<Domain.Invoice> Invoice { get; set; }

        public DbSet<Domain.Location> Location { get; set; }

        public DbSet<Domain.Policy> Policy { get; set; }

        public DbSet<Domain.Price> Price { get; set; }

        public DbSet<Domain.Property> Property { get; set; }

        public DbSet<Domain.Review> Review { get; set; }

        public DbSet<Domain.RoomFacilities> RoomFacilities { get; set; }

        public DbSet<Domain.RoomPolicies> RoomPolicies { get; set; }

        public DbSet<Domain.Reservation> Reservation { get; set; }
    }
