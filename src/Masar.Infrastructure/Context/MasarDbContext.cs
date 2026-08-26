using Masar.Application.Common.Interfaces;
using Masar.Domain.Bookings;
using Masar.Domain.Carriages;
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
using Masar.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Masar.Infrastructure.Context;

public partial class MasarDbContext : IdentityDbContext<AppUser>, IAppDbContext
{
    public MasarDbContext(DbContextOptions<MasarDbContext> options) : base(options)
    {
    }

    // DbSets الخاصة بطبقة الـ Domain
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Carriage> Carriages => Set<Carriage>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<RouteSegment> RouteSegments => Set<RouteSegment>();

    // أضف بقية الكيانات المعرفة في الـ Interface
    public DbSet<SavedPassenger> SavedPassengers => Set<SavedPassenger>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<SystemAuditLog> SystemAuditLogs => Set<SystemAuditLog>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TrainLiveLocation> TrainLiveLocations => Set<TrainLiveLocation>();
    public DbSet<Train> Trains => Set<Train>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripStop> TripStops => Set<TripStop>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    // بالنسبة لـ Users الخاضعة لـ Domain Model (تجنباً للتعارض مع Identity)
    DbSet<Domain.Users.User> IAppDbContext.Users => Set<Domain.Users.User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}