using Microsoft.EntityFrameworkCore.Migrations;

namespace DjValentin.Migrations
{
    public partial class changeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingPersons");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PersonId",
                table: "Bookings",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Persons_PersonId",
                table: "Bookings",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Persons_PersonId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PersonId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Bookings");

            migrationBuilder.CreateTable(
                name: "BookingPersons",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPersons", x => new { x.BookingId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_BookingPersons_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingPersons_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingPersons_PersonId",
                table: "BookingPersons",
                column: "PersonId");
        }
    }
}
