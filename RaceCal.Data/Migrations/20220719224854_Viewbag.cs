using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceCal.Data.Migrations
{
    public partial class Viewbag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Series",
                table: "Races");

            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "Races",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "Races",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Races_SeriesId",
                table: "Races",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_TrackId",
                table: "Races",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Serieses_SeriesId",
                table: "Races",
                column: "SeriesId",
                principalTable: "Serieses",
                principalColumn: "SeriesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Tracks_TrackId",
                table: "Races",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Serieses_SeriesId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Tracks_TrackId",
                table: "Races");

            migrationBuilder.DropIndex(
                name: "IX_Races_SeriesId",
                table: "Races");

            migrationBuilder.DropIndex(
                name: "IX_Races_TrackId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "Races");

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Races",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
