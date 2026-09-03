using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureTrackTypeAsString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RouteSegments_FromStationID",
                table: "RouteSegments");

            migrationBuilder.DropIndex(
                name: "IX_RouteSegments_ToStationID",
                table: "RouteSegments");

            migrationBuilder.AlterColumn<string>(
                name: "TrackType",
                table: "RouteSegments",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Single",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20)
                .Annotation("Relational:DefaultConstraintName", "DF__RouteSegments__TrackType__47FF419A");

            migrationBuilder.CreateIndex(
                name: "IX_RouteSegments_CorridorName",
                table: "RouteSegments",
                column: "CorridorName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteSegments_FromStationID_ToStationID",
                table: "RouteSegments",
                columns: new[] { "FromStationID", "ToStationID" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_RouteSegments_TrackType",
                table: "RouteSegments",
                sql: "[TrackType] IN ('Single', 'Double')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RouteSegments_CorridorName",
                table: "RouteSegments");

            migrationBuilder.DropIndex(
                name: "IX_RouteSegments_FromStationID_ToStationID",
                table: "RouteSegments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_RouteSegments_TrackType",
                table: "RouteSegments");

            migrationBuilder.AlterColumn<string>(
                name: "TrackType",
                table: "RouteSegments",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldDefaultValue: "Single")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__RouteSegments__TrackType__47FF419A");

            migrationBuilder.CreateIndex(
                name: "IX_RouteSegments_FromStationID",
                table: "RouteSegments",
                column: "FromStationID");
        }
    }
}
