using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaterialManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "Panels",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Panels",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Panels");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Panels");
        }
    }
}
