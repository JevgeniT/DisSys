using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Availability = Domain.Availability;
using Extra = Domain.Extra;
using Facility = Domain.Facility;
using Policy = Domain.Policy;
using Property = Domain.Property;
using PropertyRules = Domain.PropertyRules;
using Reservation = Domain.Reservation;
using ReservationExtras = Domain.ReservationExtras;
using ReservationRooms = Domain.ReservationRooms;
using Review = Domain.Review;
using Room = Domain.Room;
using RoomFacilities = Domain.RoomFacilities;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {
        private readonly IUserNameProvider _userNameProvider;

        private readonly Dictionary<IDomainBaseEntity<Guid>, IDomainBaseEntity<Guid>> _entityTracker = new ();
        
        public DbSet<Extra> Extras { get; set; } = default!;
        public DbSet<Facility> Facilities { get; set; } = default!;

        public DbSet<Reservation> Reservations { get; set; } = default!;
        public DbSet<ReservationRooms> ReservationRooms { get; set; } = default!;
        public DbSet<ReservationExtras> ReservationExtras { get; set; } = default!;

        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Policy> Policies { get; set; } = default!;
        public DbSet<RoomFacilities> RoomFacilities { get; set; } = default!;
        public DbSet<PropertyRules> PropertyRules { get; set; } = default!;
        public DbSet<Availability> Availabilities { get; set; } = default!;
        public DbSet<Property> Properties { get; set; } = default!;
        public DbSet<Room> Rooms { get; set; } = default!;


        public AppDbContext(DbContextOptions<AppDbContext>? options, IUserNameProvider userNameProvider) : base(options)
        {
           _userNameProvider = userNameProvider;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           base.OnModelCreating(builder);
           
           builder.Entity<Property>()
               .HasOne(b => b.PropertyRules)
               .WithOne(i => i!.Property!)
               .HasForeignKey<PropertyRules>(b => b.Id);
           
           builder.Entity<PropertyRules>().Property(r=> r.PaymentMethodsAccepted).HasConversion(
               v => string.Join(',', v!),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

           builder.Entity<Room>().Property(r=> r.BedTypes).HasConversion(
               v => string.Join(',', v!),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

           builder.Entity<Reservation>().Property(r => r.Status)
               .HasConversion<string>();
           
           BaseDataProvider.SeedIdentity(builder);
           BaseDataProvider.SeedFacilities(builder);

           foreach (var relationship in builder.Model
               .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
           {
               relationship.DeleteBehavior = DeleteBehavior.Restrict;
           }
        }
         public void AddToEntityTracker(IDomainBaseEntity<Guid> internalEntity, IDomainBaseEntity<Guid> externalEntity)
         {
             _entityTracker.Add(internalEntity, externalEntity);
         }

        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker) value.Id = key.Id;
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new ())
        {
            SaveChangesMetadataUpdate();
            var result = await base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            return result;
        }
        
        private void SaveChangesMetadataUpdate()
        {
            // update the state of ef tracked objects
            ChangeTracker.DetectChanges();

            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (entityEntry.Entity is not IDomainEntityMetadata entityWithMetaData) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
                entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (entityEntry.Entity is not IDomainEntityMetadata entityWithMetaData) continue;

                entityWithMetaData.ChangedAt = DateTime.Now;
                entityWithMetaData.ChangedBy = _userNameProvider.CurrentUserName;

                // do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
        }
    }
}