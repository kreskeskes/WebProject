using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_ProductCategories_ProductCategoryId",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_Products_ProductId",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeProductCategory_ProductCategories_ProductCategoryId",
                table: "ProductTypeProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeProductCategory_ProductTypes_ProductTypeId",
                table: "ProductTypeProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypeProductCategory",
                table: "ProductTypeProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductCategory",
                table: "ProductProductCategory");

            migrationBuilder.RenameTable(
                name: "ProductTypeProductCategory",
                newName: "ProductTypeProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductProductCategory",
                newName: "ProductProductCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeProductCategory_ProductCategoryId",
                table: "ProductTypeProductCategories",
                newName: "IX_ProductTypeProductCategories_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductCategory_ProductId",
                table: "ProductProductCategories",
                newName: "IX_ProductProductCategories_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypeProductCategories",
                table: "ProductTypeProductCategories",
                columns: new[] { "ProductTypeId", "ProductCategoryId" });

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
                name: "FK_ProductTypeProductCategories_ProductCategories_ProductCategoryId",
                table: "ProductTypeProductCategories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeProductCategories_ProductTypes_ProductTypeId",
                table: "ProductTypeProductCategories",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategories_ProductCategories_ProductCategoryId",
                table: "ProductProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategories_Products_ProductId",
                table: "ProductProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeProductCategories_ProductCategories_ProductCategoryId",
                table: "ProductTypeProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeProductCategories_ProductTypes_ProductTypeId",
                table: "ProductTypeProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypeProductCategories",
                table: "ProductTypeProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductCategories",
                table: "ProductProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductTypeProductCategories",
                newName: "ProductTypeProductCategory");

            migrationBuilder.RenameTable(
                name: "ProductProductCategories",
                newName: "ProductProductCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypeProductCategories_ProductCategoryId",
                table: "ProductTypeProductCategory",
                newName: "IX_ProductTypeProductCategory_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductCategories_ProductId",
                table: "ProductProductCategory",
                newName: "IX_ProductProductCategory_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypeProductCategory",
                table: "ProductTypeProductCategory",
                columns: new[] { "ProductTypeId", "ProductCategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductCategory",
                table: "ProductProductCategory",
                columns: new[] { "ProductCategoryId", "ProductId" });

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
                name: "FK_ProductTypeProductCategory_ProductCategories_ProductCategoryId",
                table: "ProductTypeProductCategory",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeProductCategory_ProductTypes_ProductTypeId",
                table: "ProductTypeProductCategory",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
