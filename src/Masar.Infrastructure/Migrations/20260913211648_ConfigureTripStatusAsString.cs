using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureTripStatusAsString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedPassengers_Passengers_UserID",
                table: "SavedPassengers");

            migrationBuilder.RenameIndex(
                name: "IX_SavedPassengers_UserID",
                table: "SavedPassengers",
                newName: "IX_SavedPassengers_PassengerId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Trips",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Scheduled",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldDefaultValue: "Scheduled")
                .Annotation("Relational:DefaultConstraintName", "DF__Trips__Status__87FF419A");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Trips_Status",
                table: "Trips",
                sql: "[Status] IN ('Scheduled','OnTheWay','Completed','Cancelled')");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPassengers_Passengers_PassengerId",
                table: "SavedPassengers",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedPassengers_Passengers_PassengerId",
                table: "SavedPassengers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Trips_Status",
                table: "Trips");

            migrationBuilder.RenameIndex(
                name: "IX_SavedPassengers_PassengerId",
                table: "SavedPassengers",
                newName: "IX_SavedPassengers_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Trips",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "Scheduled",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldDefaultValue: "Scheduled")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Trips__Status__87FF419A");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPassengers_Passengers_UserID",
                table: "SavedPassengers",
                column: "UserID",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
