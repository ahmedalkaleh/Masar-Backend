using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Trains__Curre__5EB337D6",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Trains")
                .Annotation("Relational:DefaultConstraintName", "DF_Trains_IsActive");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__Users__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "TripStops",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__TripStops__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Trips",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__Trips__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Trains",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "0 -> Active, 1 -> Inactive, 2 -> Maintenance, 3 -> Cancelled.",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldDefaultValue: "Active")
                .Annotation("Relational:DefaultConstraintName", "DF__Trains__Status__44FF419A")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Trains__Status__44FF419A");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Trains",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__Trains__IsDelete__45E365D3");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentStationId",
                table: "Trains",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "TrainLiveLocations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__TrainLivedLocations__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__Tickets__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Stations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__Stations__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__Seats__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "RouteSegments",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__RouteSegments__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Carriages",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__Carriages__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:DefaultConstraintName", "DF__Bookings__IsDelete__45F365D3");

            migrationBuilder.AddForeignKey(
                name: "FK__Trains__Curre__5EB337D6",
                table: "Trains",
                column: "CurrentStationId",
                principalTable: "Stations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Trains__Curre__5EB337D6",
                table: "Trains");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Users__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "TripStops",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__TripStops__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Trips",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Trips__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Trains",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)0,
                oldComment: "0 -> Active, 1 -> Inactive, 2 -> Maintenance, 3 -> Cancelled.")
                .Annotation("Relational:DefaultConstraintName", "DF__Trains__Status__44FF419A")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Trains__Status__44FF419A");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Trains",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Trains__IsDelete__45E365D3");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentStationId",
                table: "Trains",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Trains",
                type: "bit",
                nullable: false,
                defaultValue: true)
                .Annotation("Relational:DefaultConstraintName", "DF_Trains_IsActive");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "TrainLiveLocations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__TrainLivedLocations__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Tickets",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Tickets__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Stations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Stations__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Seats",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Seats__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "RouteSegments",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__RouteSegments__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Carriages",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Carriages__IsDelete__45F365D3");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "Bookings",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false)
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Bookings__IsDelete__45F365D3");

            migrationBuilder.AddForeignKey(
                name: "FK__Trains__Curre__5EB337D6",
                table: "Trains",
                column: "CurrentStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
