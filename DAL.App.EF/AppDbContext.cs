using System;
 using System.Collections.Generic;
 using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {
        private IUserNameProvider _userNameProvider;

        private readonly Dictionary<IDomainBaseEntity<Guid>, IDomainBaseEntity<Guid>> _entityTracker =
            new Dictionary<IDomainBaseEntity<Guid>, IDomainBaseEntity<Guid>>();
        
        public DbSet<Extra> Extras { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationRooms> ReservationRooms { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<RoomFacilities> RoomFacilities { get; set; }
        public DbSet<PropertyRules> PropertyRules { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Room> Rooms { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext>? options, IUserNameProvider? userNameProvider)
           : base(options)
        {
           _userNameProvider = userNameProvider;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           base.OnModelCreating(builder);
           
           builder.Entity<Property>()
               .HasOne(b => b.PropertyRules)
               .WithOne(i => i.Property)
               .HasForeignKey<PropertyRules>(b => b.Id);
           
           builder.Entity<PropertyRules>().Property(r=> r.PaymentMethodsAccepted).HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
           
           builder.Entity<Room>().Property(room => room.Bed)
               .HasConversion(type => type.ToString(),
               type =>  (BedType)Enum.Parse(typeof(BedType),type));
           
           BaseDateProvider.SeedIdentity(builder);
           
           BaseDateProvider.SeedFacilities(builder);

           foreach (var relationship in builder.Model
               .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
           {
               relationship.DeleteBehavior = DeleteBehavior.Restrict;
           }
        }
         public void AddToEntityTracker(IDomainBaseEntity<Guid> internalEntity,
                    IDomainBaseEntity<Guid> externalEntity)
         {
             _entityTracker.Add(internalEntity, externalEntity);
         }

        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }
        

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChangesAsync(cancellationToken);
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
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
                entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.ChangedAt = DateTime.Now;
                entityWithMetaData.ChangedBy = _userNameProvider.CurrentUserName;

                // do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
        }


       
    }
}