using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HF6Svende.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImageFileUpdateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "File",
                table: "Images",
                type: "VARBINARY(MAX)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "File",
                table: "Images",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY(MAX)");
        }
    }
}
