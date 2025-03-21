using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Migrations
{
    /// <inheritdoc />
    public partial class changes_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Bookings_BookingID",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Bookings_BookingID",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "Rooms",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_BookingID",
                table: "Rooms",
                newName: "IX_Rooms_BookingId");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "Guests",
                newName: "BookingId");

            migrationBuilder.RenameColumn(
                name: "GuestID",
                table: "Guests",
                newName: "GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_BookingID",
                table: "Guests",
                newName: "IX_Guests_BookingId");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "Bookings",
                newName: "BookingId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Bookings_BookingId",
                table: "Guests",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Bookings_BookingId",
                table: "Rooms",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Bookings_BookingId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Bookings_BookingId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Rooms",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_BookingId",
                table: "Rooms",
                newName: "IX_Rooms_BookingID");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Guests",
                newName: "BookingID");

            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "Guests",
                newName: "GuestID");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_BookingId",
                table: "Guests",
                newName: "IX_Guests_BookingID");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Bookings",
                newName: "BookingID");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Bookings_BookingID",
                table: "Guests",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Bookings_BookingID",
                table: "Rooms",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
