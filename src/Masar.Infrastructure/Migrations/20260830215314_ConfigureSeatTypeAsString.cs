using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureSeatTypeAsString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnPosition",
                table: "Seats");

            migrationBuilder.AlterColumn<string>(
                name: "SeatType",
                table: "Seats",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldDefaultValue: "Standard")
                .Annotation("Relational:DefaultConstraintName", "DF__Seats__SeatType__45A365D3");

            migrationBuilder.AlterColumn<string>(
                name: "RowNumber",
                table: "Seats",
                type: "varchar(2)",
                unicode: false,
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "ColumnNumber",
                table: "Seats",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "UQ_Carriage_Seat_1",
                table: "Seats",
                columns: new[] { "CarriageID", "RowNumber", "ColumnNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Carriage_Seat_1",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "ColumnNumber",
                table: "Seats");

            migrationBuilder.AlterColumn<string>(
                name: "SeatType",
                table: "Seats",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "Standard",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldDefaultValue: "Normal")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Seats__SeatType__45A365D3");

            migrationBuilder.AlterColumn<int>(
                name: "RowNumber",
                table: "Seats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldUnicode: false,
                oldMaxLength: 2);

            migrationBuilder.AddColumn<string>(
                name: "ColumnPosition",
                table: "Seats",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
