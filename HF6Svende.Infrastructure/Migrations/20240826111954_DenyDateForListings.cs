using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HF6Svende.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DenyDateForListings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DenyDate",
                table: "Listings",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DenyDate",
                table: "Listings");
        }
    }
}
