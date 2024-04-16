using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class SystemCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodeDetails_systemCodes_SystemCodeId",
                table: "SystemCodeDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_systemCodes",
                table: "systemCodes");

            migrationBuilder.RenameTable(
                name: "systemCodes",
                newName: "SystemCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemCodes",
                table: "SystemCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodeDetails_SystemCodes_SystemCodeId",
                table: "SystemCodeDetails",
                column: "SystemCodeId",
                principalTable: "SystemCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodeDetails_SystemCodes_SystemCodeId",
                table: "SystemCodeDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemCodes",
                table: "SystemCodes");

            migrationBuilder.RenameTable(
                name: "SystemCodes",
                newName: "systemCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_systemCodes",
                table: "systemCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodeDetails_systemCodes_SystemCodeId",
                table: "SystemCodeDetails",
                column: "SystemCodeId",
                principalTable: "systemCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
