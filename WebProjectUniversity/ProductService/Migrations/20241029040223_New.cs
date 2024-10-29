using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryIds",
                table: "Products");

            migrationBuilder.InsertData(
                table: "ProductProductCategory",
                columns: new[] { "ProductCategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("78c27d10-b5ec-435d-ae90-60211124db26"), new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3") },
                    { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("4a1d0cfc-c4e1-4b4e-b8a7-3fcab13b6cf9") },
                    { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3") },
                    { new Guid("f5c8a789-1234-4abc-9def-456789012345"), new Guid("2dd55f91-1655-42d6-9c2b-7f1a1bfa6184") },
                    { new Guid("f5c8a789-1234-4abc-9def-456789012345"), new Guid("e884afb9-b3b5-40b4-9776-cdafeb392382") }
                });

            migrationBuilder.InsertData(
                table: "ProductTypeProductCategory",
                columns: new[] { "ProductCategoryId", "ProductTypeId" },
                values: new object[,]
                {
                    { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("15f1fbdc-9a24-4e9b-a47e-ecf8f95c5a43") },
                    { new Guid("f5c8a789-1234-4abc-9def-456789012345"), new Guid("28b1c40d-56c0-4b39-b9b4-f0b6e1c5b2c7") },
                    { new Guid("78c27d10-b5ec-435d-ae90-60211124db26"), new Guid("7f7e91a5-1b2b-4c6d-a99f-d1f2e5c79f35") },
                    { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("7f7e91a5-1b2b-4c6d-a99f-d1f2e5c79f35") },
                    { new Guid("f5c8a789-1234-4abc-9def-456789012345"), new Guid("efd580ad-6076-4008-a2bc-03ff97507bb6") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("78c27d10-b5ec-435d-ae90-60211124db26"), new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3") });

            migrationBuilder.DeleteData(
                table: "ProductProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("4a1d0cfc-c4e1-4b4e-b8a7-3fcab13b6cf9") });

            migrationBuilder.DeleteData(
                table: "ProductProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3") });

            migrationBuilder.DeleteData(
                table: "ProductProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("f5c8a789-1234-4abc-9def-456789012345"), new Guid("2dd55f91-1655-42d6-9c2b-7f1a1bfa6184") });

            migrationBuilder.DeleteData(
                table: "ProductProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("f5c8a789-1234-4abc-9def-456789012345"), new Guid("e884afb9-b3b5-40b4-9776-cdafeb392382") });

            migrationBuilder.DeleteData(
                table: "ProductTypeProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductTypeId" },
                keyValues: new object[] { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("15f1fbdc-9a24-4e9b-a47e-ecf8f95c5a43") });

            migrationBuilder.DeleteData(
                table: "ProductTypeProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductTypeId" },
                keyValues: new object[] { new Guid("f5c8a789-1234-4abc-9def-456789012345"), new Guid("28b1c40d-56c0-4b39-b9b4-f0b6e1c5b2c7") });

            migrationBuilder.DeleteData(
                table: "ProductTypeProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductTypeId" },
                keyValues: new object[] { new Guid("78c27d10-b5ec-435d-ae90-60211124db26"), new Guid("7f7e91a5-1b2b-4c6d-a99f-d1f2e5c79f35") });

            migrationBuilder.DeleteData(
                table: "ProductTypeProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductTypeId" },
                keyValues: new object[] { new Guid("b75f9050-7a85-4d1e-bd86-63e1fa4bdb54"), new Guid("7f7e91a5-1b2b-4c6d-a99f-d1f2e5c79f35") });

            migrationBuilder.DeleteData(
                table: "ProductTypeProductCategory",
                keyColumns: new[] { "ProductCategoryId", "ProductTypeId" },
                keyValues: new object[] { new Guid("f5c8a789-1234-4abc-9def-456789012345"), new Guid("efd580ad-6076-4008-a2bc-03ff97507bb6") });

            migrationBuilder.AddColumn<string>(
                name: "CategoryIds",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2dd55f91-1655-42d6-9c2b-7f1a1bfa6184"),
                column: "CategoryIds",
                value: "[\"f5c8a789-1234-4abc-9def-456789012345\"]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4a1d0cfc-c4e1-4b4e-b8a7-3fcab13b6cf9"),
                column: "CategoryIds",
                value: "[\"78c27d10-b5ec-435d-ae90-60211124db26\",\"b75f9050-7a85-4d1e-bd86-63e1fa4bdb54\"]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e884afb9-b3b5-40b4-9776-cdafeb392382"),
                column: "CategoryIds",
                value: "[\"f5c8a789-1234-4abc-9def-456789012345\"]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e95b4df5-8f24-4d1a-9b0a-0eebc6a9e1f3"),
                column: "CategoryIds",
                value: "[\"78c27d10-b5ec-435d-ae90-60211124db26\",\"b75f9050-7a85-4d1e-bd86-63e1fa4bdb54\"]");
        }
    }
}
