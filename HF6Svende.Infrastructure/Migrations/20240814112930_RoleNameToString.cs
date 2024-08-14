using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HF6Svende.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleNameToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Listing_ListingId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listing_ListingId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Listing_Customers_CustomerId",
                table: "Listing");

            migrationBuilder.DropForeignKey(
                name: "FK_Listing_Products_ProductId",
                table: "Listing");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Listing_ListingId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listing",
                table: "Listing");

            migrationBuilder.RenameTable(
                name: "Listing",
                newName: "Listings");

            migrationBuilder.RenameIndex(
                name: "IX_Listing_ProductId",
                table: "Listings",
                newName: "IX_Listings_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Listing_CustomerId",
                table: "Listings",
                newName: "IX_Listings_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listings",
                table: "Listings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Listings_ListingId",
                table: "Carts",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Customers_CustomerId",
                table: "Listings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Products_ProductId",
                table: "Listings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Listings_ListingId",
                table: "OrderItems",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Listings_ListingId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Customers_CustomerId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Products_ProductId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Listings_ListingId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Listings",
                table: "Listings");

            migrationBuilder.RenameTable(
                name: "Listings",
                newName: "Listing");

            migrationBuilder.RenameIndex(
                name: "IX_Listings_ProductId",
                table: "Listing",
                newName: "IX_Listing_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Listings_CustomerId",
                table: "Listing",
                newName: "IX_Listing_CustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Roles",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listing",
                table: "Listing",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Listing_ListingId",
                table: "Carts",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listing_ListingId",
                table: "Images",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listing_Customers_CustomerId",
                table: "Listing",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Listing_Products_ProductId",
                table: "Listing",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Listing_ListingId",
                table: "OrderItems",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
