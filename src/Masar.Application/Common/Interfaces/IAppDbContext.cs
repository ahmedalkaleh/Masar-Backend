using System;
using System.Collections.Generic;
using System.Text;
using Masar.Domain.Person;
using Masar.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Masar.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Booking> Bookings { get; }
        DbSet<Carriage> Carriages { get; }
        DbSet<Passenger> Passengers { get; }
        DbSet<Person> Persons { get; }
        DbSet<Role> Roles { get; }
        DbSet<RouteSegment> RouteSegments { get; }
        DbSet<SavedPassenger> SavedPassengers { get; }
        DbSet<Seat> Seats { get; }
        DbSet<Station> Stations { get; }
        DbSet<SystemAuditLog> SystemAuditLogs { get; }
        DbSet<Ticket> Tickets { get; }
        DbSet<Train> Trains { get; }
        DbSet<TrainLiveLocation> TrainLiveLocations { get; }
        DbSet<Trip> Trips { get; }
        DbSet<TripStop> TripStops { get; }
        DbSet<User> Users { get; }
    }
}
