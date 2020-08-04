using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedProductTransalation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3d8f410d-11d4-4620-b28d-766efce8cad4");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1a5c338-27cf-488d-9523-be8f830f2a85", "AQAAAAEAACcQAAAAEFm6bMLoKejeo/YXqBkjm/pNrLQkgfzVw5d2bOpHONUlVak/7ZAMrqoNlO+9NXymog==" });

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, "Áo Nam mua hè", "Áo Nam mua hè 2020", "vi-VN", "Áo Nam", 1, "ao_nam", null, null },
                    { 2, "Men Shirt For Summer", "Men Shirt For Summer 2020", "en-US", "Men Shirt", 1, "men_shirt", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 5, 0, 19, 13, 238, DateTimeKind.Local).AddTicks(2525));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "b3245280-826a-4ac1-a98b-36f02208a8f0");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a79883d0-ee83-44dd-984c-a117fd788353", "AQAAAAEAACcQAAAAEImNDynvdpimgZEPlM2nbsGLMSMImUbiVwTdW4Qd2vFC0PzOcR00XnKmlf7+rlUBsA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 8, 4, 15, 11, 14, 556, DateTimeKind.Local).AddTicks(4089));
        }
    }
}
