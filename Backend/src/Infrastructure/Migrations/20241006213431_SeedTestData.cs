using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "blogPosts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Technology" },
                    { 2, "Health" },
                    { 3, "Lifestyle" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, new DateTime(2024, 10, 6, 21, 34, 30, 740, DateTimeKind.Utc).AddTicks(7744), "user1@example.com", new byte[] { 53, 156, 9, 7, 53, 155, 164, 38, 131, 86, 209, 25, 121, 130, 92, 214, 40, 15, 59, 126, 93, 70, 52, 192, 102, 160, 41, 241, 224, 53, 254, 8, 97, 31, 128, 150, 196, 23, 124, 209, 30, 220, 3, 210, 193, 252, 184, 28, 83, 82, 185, 224, 89, 96, 112, 149, 40, 203, 217, 191, 60, 224, 234, 60 }, new byte[] { 93, 242, 20, 98, 49, 81, 133, 218, 82, 30, 135, 120, 40, 24, 200, 93, 127, 20, 77, 186, 73, 97, 40, 37, 190, 105, 205, 81, 39, 171, 189, 178, 58, 215, 218, 4, 209, 31, 59, 68, 158, 247, 221, 244, 252, 12, 19, 83, 122, 79, 149, 112, 164, 169, 9, 250, 104, 179, 24, 196, 8, 104, 247, 140, 246, 83, 158, 152, 217, 84, 216, 27, 103, 174, 189, 3, 218, 25, 137, 70, 2, 114, 30, 193, 38, 246, 148, 227, 22, 41, 153, 224, 91, 205, 245, 254, 250, 182, 191, 244, 216, 106, 113, 210, 10, 218, 76, 193, 3, 86, 109, 125, 219, 183, 135, 206, 10, 0, 66, 166, 115, 98, 74, 18, 209, 246, 203, 151 }, "User1" });

            migrationBuilder.InsertData(
                table: "blogPosts",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "LastUpdatedAt", "Title" },
                values: new object[] { 1, 1, "This is the first post.", new DateTime(2024, 10, 6, 21, 34, 30, 740, DateTimeKind.Utc).AddTicks(7785), null, "First Post" });

            migrationBuilder.InsertData(
                table: "BlogPostCategories",
                columns: new[] { "BlogPostId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "blogPosts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "blogPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
