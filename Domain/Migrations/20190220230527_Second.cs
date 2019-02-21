using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Points",
                table: "Players",
                newName: "Score");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Players",
                newName: "Points");
        }
    }
}
