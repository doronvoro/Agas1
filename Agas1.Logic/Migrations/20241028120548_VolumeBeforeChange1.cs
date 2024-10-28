using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agas1.Logic.Migrations
{
    /// <inheritdoc />
    public partial class VolumeBeforeChange1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "VolumeBeforeChange",
                table: "TankLogs",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeBeforeChange",
                table: "TankLogs");
        }
    }
}
