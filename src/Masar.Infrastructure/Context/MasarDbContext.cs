using Masar.Application.Common.Interfaces;
using Masar.Domain.Bookings;
using Masar.Domain.Carriages;
using Masar.Domain.Common;
using Masar.Domain.Identity;
using Masar.Domain.Passengers;
using Masar.Domain.Persons;
using Masar.Domain.RouteSegments;
using Masar.Domain.SavedPassengers;
using Masar.Domain.Seats;
using Masar.Domain.Stations;
using Masar.Domain.SystemAuditLogs;
using Masar.Domain.Tickets;
using Masar.Domain.TrainLiveLocations;
using Masar.Domain.Trains;
using Masar.Domain.Trips;
using Masar.Domain.TripStops;
using Masar.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Masar.Infrastructure.Identity;
using System;
using System.Collections.Generic;
namespace Masar.Infrastructure.Context;

public partial class MasarDbContext : IdentityDbContext<AppUser>, IAppDbContext
{
    public MasarDbContext()
    {
    }

    public MasarDbContext(DbContextOptions<MasarDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Carriage> Carriages { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<RouteSegment> RouteSegments { get; set; }

    public virtual DbSet<SavedPassenger> SavedPassengers { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<SystemAuditLog> SystemAuditLogs { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<TrainLiveLocation> TrainLiveLocations { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripStop> TripStops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=masar;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__73951ACD8A79C1D9");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();
            
            entity.HasIndex(e => e.BookingReference, "UQ__Bookings__F9B66F614220A891").IsUnique();

            entity.Property(e => e.AlightingStationId).HasColumnName("AlightingStationID");
            entity.Property(e => e.BoardingStationId).HasColumnName("BoardingStationID");
            entity.Property(e => e.BookingReference)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Bookings__CreatedAt__45F365D3");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TripId).HasColumnName("TripID");

            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__Bookings__IsDelete__45F365D3");

            entity.HasOne(d => d.AlightingStation).WithMany(p => p.BookingAlightingStations)
                .HasForeignKey(d => d.AlightingStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Alight__6FE99F9F");

            entity.HasOne(d => d.BoardingStation).WithMany(p => p.BookingBoardingStations)
                .HasForeignKey(d => d.BoardingStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Boardi__6EF57B66");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__PassengerID__6D0D32F4");

            entity.HasOne(d => d.Trip).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TripId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__TripID__6E01572D");
        });

        modelBuilder.Entity<Carriage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Carriage__17FE2DB09A454F45");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();


            entity.HasIndex(e => new { e.TrainId, e.CarriageNumber }, "UQ_Train_Carriage").IsUnique();

            entity.Property(e => e.ClassType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TrainId).HasColumnName("TrainID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Carriages__CreatedAt__45F365D3");
            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__Carriages__IsDelete__45F365D3");

            entity.HasOne(d => d.Train).WithMany(p => p.Carriages)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__Carriages__Train__49C3F6B7");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Customer");

            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Passengers__CreatedAt__45F365D3");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasIndex(e => e.Email, "UK_Persons_Email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Persons__CreatedAt__45F365D3");

        });

<<<<<<< HEAD
=======
        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");

            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Roles__CreatedAt__45F365D3");

        });

>>>>>>> origin/main
        modelBuilder.Entity<RouteSegment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RouteSeg__C680609B5A711C10");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.Property(e => e.CorridorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DistanceKm)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("DistanceKM");
            entity.Property(e => e.FromStationId).HasColumnName("FromStationID");
            entity.Property(e => e.ToStationId).HasColumnName("ToStationID");
            entity.Property(e => e.TrackType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__RouteSegments__CreatedAt__45F365D3");

            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__RouteSegments__IsDelete__45F365D3");

            entity.HasOne(d => d.FromStation).WithMany(p => p.RouteSegmentFromStations)
                .HasForeignKey(d => d.FromStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RouteSegm__FromS__3C69FB99");

            entity.HasOne(d => d.ToStation).WithMany(p => p.RouteSegmentToStations)
                .HasForeignKey(d => d.ToStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RouteSegm__ToSta__3D5E1FD2");
        });

        modelBuilder.Entity<SavedPassenger>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.Property(e => e.Fullname).HasMaxLength(150);
            entity.Property(e => e.NationalId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NationalID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__SavedPassengers__CreatedAt__45F365D3");


            entity.HasOne(d => d.User).WithMany(p => p.SavedPassengers)
                  .HasForeignKey(d => d.UserId);

        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seats__311713D35FB839DB");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();


            entity.HasIndex(e => new { e.CarriageId, e.SeatNumber }, "UQ_Carriage_Seat").IsUnique();

            entity.Property(e => e.CarriageId).HasColumnName("CarriageID");
            entity.Property(e => e.ColumnPosition)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SeatType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Standard");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Seats__CreatedAt__45F365D3");

            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__Seats__IsDelete__45F365D3");

            entity.HasOne(d => d.Carriage).WithMany(p => p.Seats)
                .HasForeignKey(d => d.CarriageId)
                .HasConstraintName("FK__Seats__CarriageI__4D94879B");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stations__E0D8A6DDFDE3D87F");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.Property(e => e.Governorate).HasMaxLength(50);
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.NameAr).HasMaxLength(100);
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Stations__CreatedAt__45F365D3");

            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__Stations__IsDelete__45F365D3");
        });

        modelBuilder.Entity<SystemAuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemAu__5E5499A89E782156");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EntityId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EntityID");
            entity.Property(e => e.EntityName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Timestamp).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__SystemAuditLogs__CreatedAt__45F365D3");

            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tickets__712CC627C667F94A");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.HasIndex(e => new { e.SeatId, e.StartStopOrder, e.EndStopOrder }, "IX_Tickets_Seat_Stops");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Fullname).HasMaxLength(150);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.QrcodeHash)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("QRCodeHash");
            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Valid", "DF__Tickets__Status__76969D2E");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Tickets__CreatedAt__45F365D3");

            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__Tickets__IsDelete__45F365D3");

            entity.HasOne(d => d.Booking).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Tickets__Booking__74AE54BC");

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__SeatID__75A278F5");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trains__8ED2725A7F2931CC");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.HasIndex(e => e.Code, "UQ__Trains__A25C5AA765BD0DBB").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Trains__CreatedAt__45F365D3");
            entity.Property(e => e.MaxSpeedKmh)
                .HasDefaultValue(120, "DF__Trains__MaxSpeed__440B1D61")
                .HasColumnName("MaxSpeedKMH");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasColumnName("Status")
                .HasColumnType("tinyint")
                .HasDefaultValue(TrainStatus.Active, "DF__Trains__Status__44FF419A")
                .HasComment("0 -> Active, 1 -> Inactive, 2 -> Maintenance, 3 -> Cancelled.");
            entity.ToTable(t => t.HasCheckConstraint("CK_Trains_Status", "[Status] IN (0, 1, 2, 3)"));
            entity.Property(e => e.TrainType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__Trains__IsDelete__45E365D3");

            entity.HasOne(d => d.CurrentStation).WithMany(e => e.Trains)
            .HasForeignKey(d => d.CurrentStationId)
            .HasConstraintName("FK__Trains__Curre__5EB337D6").IsRequired(false);
        });

        modelBuilder.Entity<TrainLiveLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TrainLiv__A58A3418F1E17E41");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.HasIndex(e => new { e.TripId, e.LastUpdatedUtcdatetime2 }, "IX_LiveLocations_Trip");

            entity.Property(e => e.CurrentLatitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.CurrentLongitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.CurrentSegmentId).HasColumnName("CurrentSegmentID");
            entity.Property(e => e.CurrentSpeedKmh)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("CurrentSpeedKMH");
            entity.Property(e => e.LastUpdatedUtcdatetime2)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnName("LastUpdatedUTCDatetime2");
            entity.Property(e => e.TripId).HasColumnName("TripID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__TrainLiveLocations__CreatedAt__45F365D3");

            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__TrainLivedLocations__IsDelete__45F365D3");

            entity.HasOne(d => d.CurrentSegment).WithMany(p => p.TrainLiveLocations)
                .HasForeignKey(d => d.CurrentSegmentId)
                .HasConstraintName("FK__TrainLive__Curre__60A75C0F");

            entity.HasOne(d => d.Trip).WithMany(p => p.TrainLiveLocations)
                .HasForeignKey(d => d.TripId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TrainLive__TripI__5FB337D6");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trips__51DC711E59245661");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.HasIndex(e => new { e.TrainId, e.DepartureTime, e.EstimatedArrivalTime, e.Status }, "IX_Trips_SafetyCheck");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__Trips__CreatedAt__45F365D3");
            entity.Property(e => e.DestinationStationId).HasColumnName("DestinationStationID");
            entity.Property(e => e.OriginStationId).HasColumnName("OriginStationID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Scheduled");
            entity.Property(e => e.TrainId).HasColumnName("TrainID");

            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__Trips__IsDelete__45F365D3");

            entity.HasOne(d => d.DestinationStation).WithMany(p => p.TripDestinationStations)
                .HasForeignKey(d => d.DestinationStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trips__Destinati__5441852A");

            entity.HasOne(d => d.OriginStation).WithMany(p => p.TripOriginStations)
                .HasForeignKey(d => d.OriginStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trips__OriginSta__534D60F1");

            entity.HasOne(d => d.Train).WithMany(p => p.Trips)
                .HasForeignKey(d => d.TrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trips__TrainID__52593CB8");
        });

        modelBuilder.Entity<TripStop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TripStop__4476150D060ADD54");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedNever();

            entity.HasIndex(e => new { e.TripId, e.StopOrder }, "UQ_Trip_StopOrder").IsUnique();

            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.TripId).HasColumnName("TripID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())", "DF__TripStops__CreatedAt__45F365D3");

            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__TripStops__IsDelete__45F365D3");

            entity.HasOne(d => d.Station).WithMany(p => p.TripStops)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TripStops__Stati__5BE2A6F2");

            entity.HasOne(d => d.Trip).WithMany(p => p.TripStops)
                .HasForeignKey(d => d.TripId)
                .HasConstraintName("FK__TripStops__TripI__5AEE82B9");
        });

        modelBuilder.Entity<User>(entity =>
        {
            // Primary Key
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .ValueGeneratedNever(); // لأن المفتاح يُولد بـ Guid في الـ Domain

            // Properties
            entity.Property(e => e.Username)
                  .IsRequired()
                  .HasMaxLength(50);

<<<<<<< HEAD
            entity.Property(e => e.PersonId)
                  .HasColumnName("PersonID")
                  .IsRequired();
            // حفظ الـ Enum كـ string بداخل الداتابيز (أوضح للأدمن)
            entity.Property(u => u.Role)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();
=======
            entity.Property(e => e.IsDelete).HasDefaultValue(0, "DF__Users__IsDelete__45F365D3");

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Persons");
>>>>>>> origin/main

            // قم بإزالة كود .HasOne(u => u.Role) بالكامل!

            entity.Property(e => e.IsDelete)
                  .HasDefaultValue(false);

            // Auditable Attributes (إعادة ضبط مع معايير المشروع)
            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            // 1-to-1 Relationship مع Person
            entity.HasOne(u => u.Person)
                  .WithOne() // أو مع .WithOne(p => p.User) إن أضفت الخاصية في Person
                  .HasForeignKey<User>(u => u.PersonId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Many-to-One Relationship مع Role
        });
        modelBuilder.Entity<RefreshToken>(builder =>
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(rt => rt.Id).IsClustered(false);

            builder.Property(rt => rt.Token).HasMaxLength(200);

            builder.HasIndex(rt => rt.Token).IsUnique();

            builder.Property(rt => rt.UserId).IsRequired();

            builder.Property(rt => rt.ExpiresOnUtc).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    // entry.Entity.CreatedBy = _currentUserService.UserId; // يمكن ربطه لاحقاً
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    // entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

}
