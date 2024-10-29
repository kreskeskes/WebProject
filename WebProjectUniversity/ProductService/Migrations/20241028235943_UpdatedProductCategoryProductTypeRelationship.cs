using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProductCategoryProductTypeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategories_ProductCategories_ProductCategoryId",
                table: "ProductProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategories_Products_ProductId",
                table: "ProductProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductCategories",
                table: "ProductProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductProductCategories",
                newName: "ProductProductCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductCategories_ProductId",
                table: "ProductProductCategory",
                newName: "IX_ProductProductCategory_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductCategory",
                table: "ProductProductCategory",
                columns: new[] { "ProductCategoryId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "ProductTypeProductCategory",
                columns: table => new
                {
                    ProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeProductCategory", x => new { x.ProductTypeId, x.ProductCategoryId });
                    table.ForeignKey(
                        name: "FK_ProductTypeProductCategory_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTypeProductCategory_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeProductCategory_ProductCategoryId",
                table: "ProductTypeProductCategory",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_ProductCategories_ProductCategoryId",
                table: "ProductProductCategory",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_Products_ProductId",
                table: "ProductProductCategory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_ProductCategories_ProductCategoryId",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_Products_ProductId",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypeProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductCategory",
                table: "ProductProductCategory");

            migrationBuilder.RenameTable(
                name: "ProductProductCategory",
                newName: "ProductProductCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductCategory_ProductId",
                table: "ProductProductCategories",
                newName: "IX_ProductProductCategories_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductCategories",
                table: "ProductProductCategories",
                columns: new[] { "ProductCategoryId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategories_ProductCategories_ProductCategoryId",
                table: "ProductProductCategories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategories_Products_ProductId",
                table: "ProductProductCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
