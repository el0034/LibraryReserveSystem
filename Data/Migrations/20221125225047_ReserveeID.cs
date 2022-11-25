using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryReservedSystem.Data.Migrations
{
    public partial class ReserveeID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "studIDCheckIO",
                table: "Book",
                newName: "ReserveeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReserveeID",
                table: "Book",
                newName: "studIDCheckIO");
        }
    }
}
