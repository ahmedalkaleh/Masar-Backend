using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureClassTypeAsStringAndAddConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "TotalSeats",
                table: "Carriages",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ClassType",
                table: "Carriages",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                defaultValue: "Economy",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Carriages_CarriageNumber",
                table: "Carriages",
                sql: "[CarriageNumber] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Carriages_ClassType",
                table: "Carriages",
                sql: "[ClassType] IN ('Economy', 'VIP')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Carriages_TotalSeats",
                table: "Carriages",
                sql: "[TotalSeats] Between 1 and 500");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Carriages_CarriageNumber",
                table: "Carriages");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Carriages_ClassType",
                table: "Carriages");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Carriages_TotalSeats",
                table: "Carriages");

            migrationBuilder.AlterColumn<int>(
                name: "TotalSeats",
                table: "Carriages",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");

            migrationBuilder.AlterColumn<string>(
                name: "ClassType",
                table: "Carriages",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldUnicode: false,
                oldMaxLength: 15,
                oldDefaultValue: "Economy");
        }
    }
}
