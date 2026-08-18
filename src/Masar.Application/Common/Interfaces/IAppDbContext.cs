using System;
using System.Collections.Generic;
using System.Text;
using Masar.Domain.Persons;
using Masar.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Masar.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
       public DbSet<Booking> Bookings { get; }
       public DbSet<Carriage> Carriages { get; }
       public DbSet<Passenger> Passengers { get; }
       public DbSet<Person> Persons { get; }
       public DbSet<Role> Roles { get; }
       public DbSet<RouteSegment> RouteSegments { get; }
       public DbSet<SavedPassenger> SavedPassengers { get; }
       public DbSet<Seat> Seats { get; }
       public DbSet<Station> Stations { get; }
       public DbSet<SystemAuditLog> SystemAuditLogs { get; }
       public DbSet<Ticket> Tickets { get; }
       public DbSet<Train> Trains { get; }
       public DbSet<TrainLiveLocation> TrainLiveLocations { get; }
       public DbSet<Trip> Trips { get; }
      public  DbSet<TripStop> TripStops { get; }
      public  DbSet<User> Users { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
