using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedPassengers_User_UserID",
                table: "SavedPassengers");

            migrationBuilder.DropColumn(
                name: "IsCustomsCheck",
                table: "TripStops");

            migrationBuilder.RenameColumn(
                name: "UserId",
                newName: "PassengerId",
                table: "SavedPassengers");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPassengers_Passengers_UserID",
                table: "SavedPassengers",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedPassengers_Passengers_UserID",
                table: "SavedPassengers");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomsCheck",
                table: "TripStops",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPassengers_User_UserID",
                table: "SavedPassengers",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
