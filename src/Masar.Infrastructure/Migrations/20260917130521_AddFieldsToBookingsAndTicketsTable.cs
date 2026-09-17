using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToBookingsAndTicketsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldDefaultValue: "Valid")
                .Annotation("Relational:DefaultConstraintName", "DF__Tickets__Status__76969D2E")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Tickets__Status__76969D2E");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Bookings",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldDefaultValue: "Pending");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(minute,10,GETUTCDATE())")
                .Annotation("Relational:DefaultConstraintName", "DF__Bookings__ExpiresAt__45F365D3");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidAt",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Tickets_Status",
                table: "Tickets",
                sql: "[Status] IN ('Pending', 'Confirmed', 'Used', 'Expired', 'Cancelled')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Bookings_PaymentStatus",
                table: "Bookings",
                sql: "[PaymentStatus] IN ('Pending', 'Paid', 'Failed', 'Expired')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Tickets_Status",
                table: "Tickets");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Bookings_PaymentStatus",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "Bookings")
                .Annotation("Relational:DefaultConstraintName", "DF__Bookings__ExpiresAt__45F365D3");

            migrationBuilder.DropColumn(
                name: "PaidAt",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "Valid",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldUnicode: false,
                oldMaxLength: 15,
                oldDefaultValue: "Pending")
                .Annotation("Relational:DefaultConstraintName", "DF__Tickets__Status__76969D2E")
                .OldAnnotation("Relational:DefaultConstraintName", "DF__Tickets__Status__76969D2E");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Bookings",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldUnicode: false,
                oldMaxLength: 15,
                oldDefaultValue: "Pending");
        }
    }
}
