using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebProjectUniversity.Infrastructure.Migrations
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
                name: "ProductSubcategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubcategories_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductCategoryIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductSubcategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgeGenderGroup = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductSubcategories_ProductSubcategoryId",
                        column: x => x.ProductSubcategoryId,
                        principalTable: "ProductSubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductCategory", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_Products_ProductId",
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
                    { new Guid("4a1d0cfc-c4e1-4b4e-b8a7-3fcab13b6cf9"), "Summer Dress" },
                    { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), "Clothing" },
                    { new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3"), "Graphic Tee" },
                    { new Guid("f4d38c7a-14d2-4f2c-8738-df1d6b57c4bc"), "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "ProductSubcategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("15f1fbdc-9a24-4e9b-a47e-ecf8f95c5a43"), new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), "Dresses" },
                    { new Guid("7f7e91a5-1b2b-4c6d-a99f-d1f2e5c79f35"), new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), "T-Shirts" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeGenderGroup", "Brand", "Color", "Description", "Name", "Price", "ProductCategoryIds", "ProductSubcategoryId", "Sizes" },
                values: new object[,]
                {
                    { new Guid("4a1d0cfc-c4e1-4b4e-b8a7-3fcab13b6cf9"), 1, "Fashionista", "Red", "A stylish summer dress.", "Summer Dress", 49.99m, "[\"b75f9050-7a85-4d1e-bd86-63e1fa4bdb54\"]", new Guid("15f1fbdc-9a24-4e9b-a47e-ecf8f95c5a43"), "[2,3]" },
                    { new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3"), 0, "CoolBrand", "Black", "A cool graphic t-shirt.", "Graphic Tee", 29.99m, "[\"b75f9050-7a85-4d1e-bd86-63e1fa4bdb54\",\"f4d38c7a-14d2-4f2c-8738-df1d6b57c4bc\"]", new Guid("7f7e91a5-1b2b-4c6d-a99f-d1f2e5c79f35"), "[3,4]" }
                });

            migrationBuilder.InsertData(
                table: "ProductProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("4a1d0cfc-c4e1-4b4e-b8a7-3fcab13b6cf9") },
                    { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3") },
                    { new Guid("f4d38c7a-14d2-4f2c-8738-df1d6b57c4bc"), new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductCategory_CategoryId",
                table: "ProductProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSubcategoryId",
                table: "Products",
                column: "ProductSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubcategories_CategoryId",
                table: "ProductSubcategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductCategory");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductSubcategories");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
