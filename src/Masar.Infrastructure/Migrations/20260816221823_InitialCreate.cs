using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<byte[]>(type: "varbinary(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "SavedPassengers",
                columns: table => new
                {
                    SavedPassengerID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NationalID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HasPassingLoop = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CustomsDelayMinutes = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stations__E0D8A6DDFDE3D87F", x => x.StationID);
                });

            migrationBuilder.CreateTable(
                name: "SystemAuditLogs",
                columns: table => new
                {
                    LogID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    EntityName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    EntityID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SystemAu__5E5499A89E782156", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    TrainID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrainType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    MaxSpeedKMH = table.Column<int>(type: "int", nullable: false, defaultValue: 120)
                        .Annotation("Relational:DefaultConstraintName", "DF__Trains__MaxSpeed__440B1D61"),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Active")
                        .Annotation("Relational:DefaultConstraintName", "DF__Trains__Status__44FF419A"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                        .Annotation("Relational:DefaultConstraintName", "DF_Trains_IsActive"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())")
                        .Annotation("Relational:DefaultConstraintName", "DF__Trains__CreatedA__45F365D3"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trains__8ED2725A7F2931CC", x => x.TrainID);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.PassengerID);
                    table.ForeignKey(
                        name: "FK_Passengers_Persons",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())")
                        .Annotation("Relational:DefaultConstraintName", "DF__Users__CreatedAt__693CA210"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC2F1F502E", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Persons",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID");
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "RouteSegments",
                columns: table => new
                {
                    SegmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromStationID = table.Column<int>(type: "int", nullable: false),
                    ToStationID = table.Column<int>(type: "int", nullable: false),
                    TrackType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DistanceKM = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    EstPassengerTimeMin = table.Column<int>(type: "int", nullable: false),
                    CorridorName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RouteSeg__C680609B5A711C10", x => x.SegmentID);
                    table.ForeignKey(
                        name: "FK__RouteSegm__FromS__3C69FB99",
                        column: x => x.FromStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK__RouteSegm__ToSta__3D5E1FD2",
                        column: x => x.ToStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                });

            migrationBuilder.CreateTable(
                name: "Carriages",
                columns: table => new
                {
                    CarriageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainID = table.Column<int>(type: "int", nullable: false),
                    CarriageNumber = table.Column<int>(type: "int", nullable: false),
                    ClassType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Carriage__17FE2DB09A454F45", x => x.CarriageID);
                    table.ForeignKey(
                        name: "FK__Carriages__Train__49C3F6B7",
                        column: x => x.TrainID,
                        principalTable: "Trains",
                        principalColumn: "TrainID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainID = table.Column<int>(type: "int", nullable: false),
                    OriginStationID = table.Column<int>(type: "int", nullable: false),
                    DestinationStationID = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Scheduled"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trips__51DC711E59245661", x => x.TripID);
                    table.ForeignKey(
                        name: "FK__Trips__Destinati__5441852A",
                        column: x => x.DestinationStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK__Trips__OriginSta__534D60F1",
                        column: x => x.OriginStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK__Trips__TrainID__52593CB8",
                        column: x => x.TrainID,
                        principalTable: "Trains",
                        principalColumn: "TrainID");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarriageID = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    ColumnPosition = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SeatType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Standard"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Seats__311713D35FB839DB", x => x.SeatID);
                    table.ForeignKey(
                        name: "FK__Seats__CarriageI__4D94879B",
                        column: x => x.CarriageID,
                        principalTable: "Carriages",
                        principalColumn: "CarriageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingReference = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    PassengerID = table.Column<int>(type: "int", nullable: false),
                    TripID = table.Column<int>(type: "int", nullable: false),
                    BoardingStationID = table.Column<int>(type: "int", nullable: false),
                    AlightingStationID = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Pending"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__73951ACD8A79C1D9", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK__Bookings__Alight__6FE99F9F",
                        column: x => x.AlightingStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK__Bookings__Boardi__6EF57B66",
                        column: x => x.BoardingStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK__Bookings__PassengerID__6D0D32F4",
                        column: x => x.PassengerID,
                        principalTable: "Passengers",
                        principalColumn: "PassengerID");
                    table.ForeignKey(
                        name: "FK__Bookings__TripID__6E01572D",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "TripID");
                });

            migrationBuilder.CreateTable(
                name: "TrainLiveLocations",
                columns: table => new
                {
                    LiveLocationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripID = table.Column<int>(type: "int", nullable: false),
                    CurrentSegmentID = table.Column<int>(type: "int", nullable: true),
                    CurrentLatitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    CurrentLongitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    CurrentSpeedKMH = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DelayMinutes = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedUTCDatetime2 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrainLiv__A58A3418F1E17E41", x => x.LiveLocationID);
                    table.ForeignKey(
                        name: "FK__TrainLive__Curre__60A75C0F",
                        column: x => x.CurrentSegmentID,
                        principalTable: "RouteSegments",
                        principalColumn: "SegmentID");
                    table.ForeignKey(
                        name: "FK__TrainLive__TripI__5FB337D6",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "TripID");
                });

            migrationBuilder.CreateTable(
                name: "TripStops",
                columns: table => new
                {
                    TripStopID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripID = table.Column<int>(type: "int", nullable: false),
                    StationID = table.Column<int>(type: "int", nullable: false),
                    StopOrder = table.Column<int>(type: "int", nullable: false),
                    ScheduledArrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledDeparture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DwellTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    IsCustomsCheck = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TripStop__4476150D060ADD54", x => x.TripStopID);
                    table.ForeignKey(
                        name: "FK__TripStops__Stati__5BE2A6F2",
                        column: x => x.StationID,
                        principalTable: "Stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK__TripStops__TripI__5AEE82B9",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "TripID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StartStopOrder = table.Column<int>(type: "int", nullable: false),
                    EndStopOrder = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QRCodeHash = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Valid")
                        .Annotation("Relational:DefaultConstraintName", "DF__Tickets__Status__76969D2E"),
                    BoardedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tickets__712CC627C667F94A", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK__Tickets__Booking__74AE54BC",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Tickets__SeatID__75A278F5",
                        column: x => x.SeatID,
                        principalTable: "Seats",
                        principalColumn: "SeatID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AlightingStationID",
                table: "Bookings",
                column: "AlightingStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BoardingStationID",
                table: "Bookings",
                column: "BoardingStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PassengerID",
                table: "Bookings",
                column: "PassengerID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TripID",
                table: "Bookings",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "UQ__Bookings__F9B66F614220A891",
                table: "Bookings",
                column: "BookingReference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Train_Carriage",
                table: "Carriages",
                columns: new[] { "TrainID", "CarriageNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_PersonID",
                table: "Passengers",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "UK_Persons_Email",
                table: "Persons",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteSegments_FromStationID",
                table: "RouteSegments",
                column: "FromStationID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteSegments_ToStationID",
                table: "RouteSegments",
                column: "ToStationID");

            migrationBuilder.CreateIndex(
                name: "UQ_Carriage_Seat",
                table: "Seats",
                columns: new[] { "CarriageID", "SeatNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BookingID",
                table: "Tickets",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Seat_Stops",
                table: "Tickets",
                columns: new[] { "SeatID", "StartStopOrder", "EndStopOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_LiveLocations_Trip",
                table: "TrainLiveLocations",
                columns: new[] { "TripID", "LastUpdatedUTCDatetime2" });

            migrationBuilder.CreateIndex(
                name: "IX_TrainLiveLocations_CurrentSegmentID",
                table: "TrainLiveLocations",
                column: "CurrentSegmentID");

            migrationBuilder.CreateIndex(
                name: "UQ__Trains__A25C5AA765BD0DBB",
                table: "Trains",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationStationID",
                table: "Trips",
                column: "DestinationStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_OriginStationID",
                table: "Trips",
                column: "OriginStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_SafetyCheck",
                table: "Trips",
                columns: new[] { "TrainID", "DepartureTime", "EstimatedArrivalTime", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_TripStops_StationID",
                table: "TripStops",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "UQ_Trip_StopOrder",
                table: "TripStops",
                columns: new[] { "TripID", "StopOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonID",
                table: "Users",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedPassengers");

            migrationBuilder.DropTable(
                name: "SystemAuditLogs");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TrainLiveLocations");

            migrationBuilder.DropTable(
                name: "TripStops");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "RouteSegments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Carriages");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
