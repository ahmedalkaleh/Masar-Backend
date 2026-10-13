using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToStationID",
                table: "RouteSegments",
                newName: "SecondStationId");

            migrationBuilder.RenameColumn(
                name: "FromStationID",
                table: "RouteSegments",
                newName: "FirstStationId");


            migrationBuilder.RenameIndex(
                name: "IX_RouteSegments_FromStationID_ToStationID",
                table: "RouteSegments",
                newName: "IX_RouteSegments_FirstStationId_SecondStationId");


            // ==========================================
            // 1. Seed Stations
            // ==========================================
            var stationDamascusId = new Guid("11111111-1111-1111-1111-111111111111");
            var stationHomsId = new Guid("22222222-2222-2222-2222-222222222222");
            var stationHamaId = new Guid("33333333-3333-3333-3333-333333333333");
            var stationAleppoId = new Guid("44444444-4444-4444-4444-444444444444");
            var stationLatakiaId = new Guid("55555555-5555-5555-5555-555555555555");
            var stationTartusId = new Guid("66666666-6666-6666-6666-666666666666");
            var stationRaqqaId = new Guid("77777777-7777-7777-7777-777777777777");
            var stationDeirEzZurId = new Guid("88888888-8888-8888-8888-888888888888");
            var stationQamishliId = new Guid("99999999-9999-9999-9999-999999999999");
            var stationJablehId = new Guid("aaaaa111-1111-1111-1111-111111111111");
            var stationBaniyasId = new Guid("bbbbb222-2222-2222-2222-222222222222");
            var stationAkariId = new Guid("ccccc333-3333-3333-3333-333333333333");
            var stationPalmyraId = new Guid("ddddd444-4444-4444-4444-444444444444");

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "NameAr", "NameEn", "Type", "Latitude", "Longitude", "Governorate", "CustomsDelayMinutes", "IsDelete" },
                values: new object[,]
                {
                    { stationDamascusId, "دمشق القدم", "Damascus Al-Qadam", "Main", 33.5138m, 36.2765m, "دمشق", 0, false },
                    { stationHomsId, "حمص", "Homs", "Main", 34.7324m, 36.7137m, "حمص", 0, false },
                    { stationHamaId, "حماة", "Hama", "Main", 35.1323m, 36.7535m, "حماة", 0, false },
                    { stationAleppoId, "حلب", "Aleppo", "Main", 36.2021m, 37.1343m, "حلب", 0, false },
                    { stationLatakiaId, "اللاذقية", "Latakia", "Main", 35.5317m, 35.7904m, "اللاذقية", 0, false },
                    { stationTartusId, "طَرطُوس", "Tartus", "Main", 34.8890m, 35.8866m, "طرطوس", 0, false },
                    { stationRaqqaId, "الرقة", "Raqqa", "Main", 35.9528m, 39.0065m, "الرقة", 0, false },
                    { stationDeirEzZurId, "دير الزور", "Deir ez-Zor", "Main", 35.3359m, 40.1480m, "دير الزور", 0, false },
                    { stationQamishliId, "القائمشلي", "Qamishli", "Main", 37.0496m, 41.2177m, "الحسكة", 0, false },
                    { stationJablehId, "جبلة", "Jableh", "Sub", 35.3619m, 35.9239m, "اللاذقية", 0, false },
                    { stationBaniyasId, "بانياس", "Baniyas", "Sub", 35.1833m, 35.9500m, "طرطوس", 0, false },
                    { stationAkariId, "العكاري", "Al-Akari", "Sub", 34.6500m, 36.3833m, "حمص", 0, false },
                    { stationPalmyraId, "تدمر", "Palmyra", "Sub", 34.5623m, 38.2778m, "حمص", 0, false }
                });

            // ==========================================
            // 2. Seed Trains
            // ==========================================
            migrationBuilder.InsertData(
                table: "Trains",
                columns: new[] { "Id", "Code", "Name", "TrainType", "MaxSpeedKMH", "Status", "IsDelete", "CurrentStationId" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "MASAR-TR-001", "قطار دمشق التجريبي", "Passenger", 120, 0, false, stationDamascusId },
                    { Guid.NewGuid(), "MASAR-TR-002", "قطار حمص التجريبي", "Passenger", 120, 0, false, stationHomsId },
                    { Guid.NewGuid(), "MASAR-TR-003", "قطار حلب التجريبي", "Passenger", 120, 0, false, stationAleppoId },
                    { Guid.NewGuid(), "MASAR-TR-004", "قطار اللاذقية التجريبي", "Passenger", 120, 0, false, stationLatakiaId },
                    { Guid.NewGuid(), "MASAR-TR-005", "قطار الرقة التجريبي", "Passenger", 120, 0, false, stationRaqqaId }
                });

            // ==========================================
            // 3. Seed RouteSegments (المقاطع بين المحطات)
            // ==========================================
            migrationBuilder.InsertData(
                table: "RouteSegments",
                columns: new[] { "Id", "FirstStationId", "SecondStationId", "TrackType", "DistanceKM", "EstPassengerTimeMin", "CorridorName", "IsDelete" },
                values: new object[,]
                {
                    { Guid.NewGuid(), stationDamascusId, stationHomsId, "Single", 160.5m, 120, "Corridor-South-Center", false },
                    { Guid.NewGuid(), stationHomsId, stationHamaId, "Single", 45.0m, 40, "Corridor-Center", false },
                    { Guid.NewGuid(), stationHamaId, stationAleppoId, "Single", 140.0m, 110, "Corridor-Center-North", false },
                    { Guid.NewGuid(), stationAleppoId, stationLatakiaId, "Single", 185.0m, 160, "Corridor-North-Coast", false },
                    { Guid.NewGuid(), stationLatakiaId, stationJablehId, "Single", 25.0m, 25, "Corridor-Coast_1", false },
                    { Guid.NewGuid(), stationJablehId, stationBaniyasId, "Single", 30.0m, 30, "Corridor-Coast_2", false },
                    { Guid.NewGuid(), stationBaniyasId, stationTartusId, "Single", 35.0m, 35, "Corridor-Coast_3", false },
                    { Guid.NewGuid(), stationTartusId, stationAkariId, "Single", 40.0m, 40, "Corridor-Coast-Internal", false },
                    { Guid.NewGuid(), stationAkariId, stationHomsId, "Single", 50.0m, 45, "Corridor-Internal", false },
                    { Guid.NewGuid(), stationAleppoId, stationRaqqaId, "Single", 190.0m, 150, "Corridor-East_1", false },
                    { Guid.NewGuid(), stationRaqqaId, stationDeirEzZurId, "Single", 130.0m, 110, "Corridor-East_2", false },
                    { Guid.NewGuid(), stationDeirEzZurId, stationQamishliId, "Single", 200.0m, 180, "Corridor-Northeast", false },
                    { Guid.NewGuid(), stationHomsId, stationPalmyraId, "Single", 160.0m, 130, "Corridor-Desert", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondStationId",
                table: "RouteSegments",
                newName: "ToStationID");

            migrationBuilder.RenameColumn(
                name: "FirstStationId",
                table: "RouteSegments",
                newName: "FromStationID");


            migrationBuilder.RenameIndex(
                name: "IX_RouteSegments_FirstStationId_SecondStationId",
                table: "RouteSegments",
                newName: "IX_RouteSegments_FromStationID_ToStationID");


            migrationBuilder.DeleteData(
                table: "RouteSegments",
                keyColumn: "Id",
                keyValue: null);

            migrationBuilder.DeleteData(
                table: "Trains",
                keyColumn: "Id",
                keyValue: null);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: null);
        }



    }
}

