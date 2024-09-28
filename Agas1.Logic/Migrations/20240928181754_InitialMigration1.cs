using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agas1.Logic.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiquidAdditions_LiquidTypes_LiquidTypeId",
                table: "LiquidAdditions");

            migrationBuilder.DropForeignKey(
                name: "FK_LiquidAdditions_Tanks_TankId",
                table: "LiquidAdditions");

            migrationBuilder.DropForeignKey(
                name: "FK_TankLogs_TankProcesses_TankProcessId",
                table: "TankLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LiquidAdditions",
                table: "LiquidAdditions");

            migrationBuilder.RenameTable(
                name: "LiquidAdditions",
                newName: "LiquidAddition");

            migrationBuilder.RenameIndex(
                name: "IX_LiquidAdditions_TankId",
                table: "LiquidAddition",
                newName: "IX_LiquidAddition_TankId");

            migrationBuilder.RenameIndex(
                name: "IX_LiquidAdditions_LiquidTypeId",
                table: "LiquidAddition",
                newName: "IX_LiquidAddition_LiquidTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "TankProcessId",
                table: "TankLogs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LiquidAddition",
                table: "LiquidAddition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LiquidAddition_LiquidTypes_LiquidTypeId",
                table: "LiquidAddition",
                column: "LiquidTypeId",
                principalTable: "LiquidTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiquidAddition_Tanks_TankId",
                table: "LiquidAddition",
                column: "TankId",
                principalTable: "Tanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TankLogs_TankProcesses_TankProcessId",
                table: "TankLogs",
                column: "TankProcessId",
                principalTable: "TankProcesses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiquidAddition_LiquidTypes_LiquidTypeId",
                table: "LiquidAddition");

            migrationBuilder.DropForeignKey(
                name: "FK_LiquidAddition_Tanks_TankId",
                table: "LiquidAddition");

            migrationBuilder.DropForeignKey(
                name: "FK_TankLogs_TankProcesses_TankProcessId",
                table: "TankLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LiquidAddition",
                table: "LiquidAddition");

            migrationBuilder.RenameTable(
                name: "LiquidAddition",
                newName: "LiquidAdditions");

            migrationBuilder.RenameIndex(
                name: "IX_LiquidAddition_TankId",
                table: "LiquidAdditions",
                newName: "IX_LiquidAdditions_TankId");

            migrationBuilder.RenameIndex(
                name: "IX_LiquidAddition_LiquidTypeId",
                table: "LiquidAdditions",
                newName: "IX_LiquidAdditions_LiquidTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "TankProcessId",
                table: "TankLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LiquidAdditions",
                table: "LiquidAdditions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LiquidAdditions_LiquidTypes_LiquidTypeId",
                table: "LiquidAdditions",
                column: "LiquidTypeId",
                principalTable: "LiquidTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiquidAdditions_Tanks_TankId",
                table: "LiquidAdditions",
                column: "TankId",
                principalTable: "Tanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TankLogs_TankProcesses_TankProcessId",
                table: "TankLogs",
                column: "TankProcessId",
                principalTable: "TankProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
