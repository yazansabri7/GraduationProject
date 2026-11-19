using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorPatientCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Users_CreatedBy",
                table: "Medications");

            migrationBuilder.AddColumn<int>(
                name: "ActivePatientsCount",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxPatients",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 20);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Users_CreatedBy",
                table: "Medications",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Users_CreatedBy",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "ActivePatientsCount",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "MaxPatients",
                table: "Doctors");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Users_CreatedBy",
                table: "Medications",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
