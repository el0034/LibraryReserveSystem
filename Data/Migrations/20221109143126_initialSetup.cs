using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryReservedSystem.Data.Migrations
{
    public partial class initialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseNumber = table.Column<int>(type: "int", nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Professor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<double>(type: "float", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isCheckedOut = table.Column<bool>(type: "bit", nullable: false),
                    studIDCheckIO = table.Column<double>(type: "float", nullable: true),
                    checkOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    checkInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    numCopies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
