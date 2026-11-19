
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CheckPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Medications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_CreatedBy",
                table: "Medications",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Users_CreatedBy",
                table: "Medications",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Users_CreatedBy",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_CreatedBy",
                table: "Medications");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
