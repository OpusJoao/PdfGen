using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Singulare.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableForDeletedAtProcessReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DeletedAt",
                table: "ProcessReports",
                nullable: true
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DeletedAt",
                table: "ProcessReports",
                nullable: false
                );
        }
    }
}
