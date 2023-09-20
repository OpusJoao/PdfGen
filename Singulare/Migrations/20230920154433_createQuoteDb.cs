using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Singulare.Migrations
{
    /// <inheritdoc />
    public partial class createQuoteDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Open = table.Column<int>(type: "int", nullable: false),
                    High = table.Column<int>(type: "int", nullable: false),
                    Low = table.Column<int>(type: "int", nullable: false),
                    Close = table.Column<int>(type: "int", nullable: false),
                    AdjClose = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");
        }
    }
}
