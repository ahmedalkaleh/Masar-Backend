using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentStationIDColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentStationId",
                table: "Trains",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Trains_CurrentStationId",
                table: "Trains",
                column: "CurrentStationId");

            migrationBuilder.AddForeignKey(
                name: "FK__Trains__Curre__5EB337D6",
                table: "Trains",
                column: "CurrentStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Trains__Curre__5EB337D6",
                table: "Trains");

            migrationBuilder.DropIndex(
                name: "IX_Trains_CurrentStationId",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "CurrentStationId",
                table: "Trains");
        }
    }
}
