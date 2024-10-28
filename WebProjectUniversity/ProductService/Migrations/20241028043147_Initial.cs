using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sizes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeGenderGroup = table.Column<int>(type: "int", nullable: false),
                    Colors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Styles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductCategories", x => new { x.ProductCategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductProductCategories_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("78c27d10-b5ec-435d-ae90-60211124db26"), "Summer clothing" },
                    { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), "Clothing" },
                    { new Guid("f4d38c7a-14d2-4f2c-8738-df1d6b57c4bc"), "Accessories" },
                    { new Guid("f5c8a789-1234-4abc-9def-456789012345"), "Jewelry" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15f1fbdc-9a24-4e9b-a47e-ecf8f95c5a43"), "Dress" },
                    { new Guid("28b1c40d-56c0-4b39-b9b4-f0b6e1c5b2c7"), "Earrings" },
                    { new Guid("7f7e91a5-1b2b-4c6d-a99f-d1f2e5c79f35"), "T-Shirt" },
                    { new Guid("efd580ad-6076-4008-a2bc-03ff97507bb6"), "Necklace" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeGenderGroup", "Brand", "CategoryIds", "Colors", "Description", "Length", "Materials", "Name", "Price", "ProductTypeId", "Sizes", "Styles" },
                values: new object[,]
                {
                    { new Guid("2dd55f91-1655-42d6-9c2b-7f1a1bfa6184"), 1, "Daily Chic", "[\"f5c8a789-1234-4abc-9def-456789012345\"]", "[\"Rose Gold\",\"White\"]", "Simple and stylish stud earrings for everyday wear.", "N/A", "{\"Rose Gold\":50.0,\"Plastic\":50.0}", "Casual Stud Earrings", 29.99m, new Guid("28b1c40d-56c0-4b39-b9b4-f0b6e1c5b2c7"), "[9]", "[\"Casual\",\"Everyday\"]" },
                    { new Guid("4a1d0cfc-c4e1-4b4e-b8a7-3fcab13b6cf9"), 1, "Fashionista", "[\"78c27d10-b5ec-435d-ae90-60211124db26\",\"b75f9050-7a85-4d1e-bd86-63e1fa4bdb54\"]", "[\"Red\",\"Blue\"]", "A stylish summer dress.", "Knee-length", "{\"Cotton\":20.0,\"Silk\":80.0}", "Summer Dress", 49.99m, new Guid("15f1fbdc-9a24-4e9b-a47e-ecf8f95c5a43"), "[2,3]", "[\"Elegant\",\"Casual\"]" },
                    { new Guid("e884afb9-b3b5-40b4-9776-cdafeb392382"), 1, "Elegant Designs", "[\"f5c8a789-1234-4abc-9def-456789012345\"]", "[\"Gold\",\"Silver\"]", "Beautifully crafted gold earrings for special occasions.", "N/A", "{\"Gold\":70.0,\"Silver\":30.0}", "Elegant Necklace", 79.99m, new Guid("efd580ad-6076-4008-a2bc-03ff97507bb6"), "[9]", "[\"Elegant\",\"Formal\"]" },
                    { new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3"), 0, "CoolBrand", "[\"78c27d10-b5ec-435d-ae90-60211124db26\",\"b75f9050-7a85-4d1e-bd86-63e1fa4bdb54\"]", "[\"Black\",\"White\"]", "A cool graphic t-shirt.", "Regular", "{\"Cotton\":50.0,\"Polyester\":50.0}", "Graphic Tee", 29.99m, new Guid("7f7e91a5-1b2b-4c6d-a99f-d1f2e5c79f35"), "[3,4]", "[\"Casual\",\"Streetwear\"]" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductCategories_ProductId",
                table: "ProductProductCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductCategories");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
