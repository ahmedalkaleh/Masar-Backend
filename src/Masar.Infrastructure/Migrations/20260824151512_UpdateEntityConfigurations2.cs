using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityConfigurations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPassingLoop",
                table: "Stations");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Users",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Users",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getutcdate())")
                .Annotation("Relational:DefaultConstraintName", "DF__Users__CreatedAt__693CA210")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Users__CreatedAt__693CA210");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "TripStops",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "TripStops",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__TripStops__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Trips",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Trips",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getutcdate())")
                .Annotation("Relational:DefaultConstraintName", "DF__Trips__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Trains",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Trains",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getutcdate())")
                .Annotation("Relational:DefaultConstraintName", "DF__Trains__CreatedAt__45F365D3")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Trains__CreatedA__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "TrainLiveLocations",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "TrainLiveLocations",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__TrainLiveLocations__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Tickets",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Tickets",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__Tickets__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "SystemAuditLogs",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SystemAuditLogs",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__SystemAuditLogs__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Stations",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Stations",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getutcdate())")
                .Annotation("Relational:DefaultConstraintName", "DF__Stations__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Seats",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Seats",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__Seats__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "SavedPassengers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SavedPassengers",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__SavedPassengers__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "RouteSegments",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "RouteSegments",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__RouteSegments__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Roles",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__Roles__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Persons",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Persons",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__Persons__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Passengers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Passengers",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__Passengers__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Carriages",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Carriages",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset")
                .Annotation("Relational:DefaultConstraintName", "DF__Carriages__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Bookings",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:DefaultConstraintName", "DF__Bookings__CreatedAt__45F365D3");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Trains_Status",
                table: "Trains",
                sql: "[Status] IN (0, 1, 2, 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Trains_Status",
                table: "Trains");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Users",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .Annotation("Relational:DefaultConstraintName", "DF__Users__CreatedAt__693CA210")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Users__CreatedAt__693CA210");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "TripStops",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "TripStops",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__TripStops__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Trips",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Trips__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Trains",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Trains",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .Annotation("Relational:DefaultConstraintName", "DF__Trains__CreatedA__45F365D3")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Trains__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "TrainLiveLocations",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "TrainLiveLocations",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__TrainLiveLocations__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Tickets",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Tickets",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Tickets__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "SystemAuditLogs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SystemAuditLogs",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__SystemAuditLogs__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Stations",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Stations__CreatedAt__45F365D3");

            migrationBuilder.AddColumn<bool>(
                name: "HasPassingLoop",
                table: "Stations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Seats",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Seats",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Seats__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "SavedPassengers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SavedPassengers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__SavedPassengers__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "RouteSegments",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "RouteSegments",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__RouteSegments__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Roles",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Roles__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Persons",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Persons",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Persons__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Passengers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Passengers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Passengers__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Carriages",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Carriages",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Carriages__CreatedAt__45F365D3");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedAt",
                table: "Bookings",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "(getutcdate())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Bookings__CreatedAt__45F365D3");
        }
    }
}
