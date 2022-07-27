using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceCal.Data.Migrations
{
    public partial class deletedContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Races");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Races",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
