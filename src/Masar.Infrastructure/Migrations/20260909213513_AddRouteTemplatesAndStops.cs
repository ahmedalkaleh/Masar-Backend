using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRouteTemplatesAndStops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    StartStationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndStationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK__RouteTempl__End__4C906362",
                        column: x => x.EndStationID,
                        principalTable: "Stations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__RouteTempl__Start__4BAC3F29",
                        column: x => x.StartStationID,
                        principalTable: "Stations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RouteTemplateStops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteTemplateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StopOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteTemplateStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK__RouteTemp__Route__4E88ABD4",
                        column: x => x.RouteTemplateID,
                        principalTable: "RouteTemplates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__RouteTemp__Stati__4D94879B",
                        column: x => x.StationID,
                        principalTable: "Stations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteTemplates_EndStationID",
                table: "RouteTemplates",
                column: "EndStationID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteTemplates_StartStationID",
                table: "RouteTemplates",
                column: "StartStationID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteTemplateStop_RouteId_StopOrder",
                table: "RouteTemplateStops",
                columns: new[] { "RouteTemplateID", "StopOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteTemplateStops_StationID",
                table: "RouteTemplateStops",
                column: "StationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteTemplateStops");

            migrationBuilder.DropTable(
                name: "RouteTemplates");
        }
    }
}
