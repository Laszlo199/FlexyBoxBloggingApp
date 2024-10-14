using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                    { 3, "Lifestyle" },
                    { 4, "Science" },
                    { 5, "Education" },
                    { 6, "Travel" },
                    { 7, "Food" },
                    { 8, "Finance" },
                    { 9, "Sports" },
                    { 10, "Entertainment" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, new DateTime(2024, 10, 14, 20, 48, 4, 296, DateTimeKind.Utc).AddTicks(7935), "user1@example.com", new byte[] { 195, 218, 249, 241, 148, 149, 224, 180, 99, 41, 128, 171, 211, 115, 52, 61, 238, 7, 157, 71, 181, 85, 242, 102, 68, 226, 12, 86, 31, 122, 239, 209, 15, 185, 235, 236, 61, 223, 223, 104, 40, 245, 158, 116, 34, 89, 107, 205, 158, 45, 37, 5, 220, 223, 154, 200, 175, 78, 161, 193, 255, 93, 23, 141 }, new byte[] { 190, 72, 114, 117, 225, 148, 152, 77, 72, 83, 75, 17, 52, 188, 175, 165 }, "User1" });

            migrationBuilder.InsertData(
                table: "blogPosts",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "LastUpdatedAt", "Title" },
                values: new object[,]
                {
                    { 1, 1, "This is the first post.", new DateTime(2024, 10, 14, 20, 48, 4, 296, DateTimeKind.Utc).AddTicks(8038), null, "First Post" },
                    { 2, 1, "This is the second post.", new DateTime(2024, 10, 14, 20, 48, 4, 296, DateTimeKind.Utc).AddTicks(8043), null, "Second Post" },
                    { 3, 1, "This is the third post.", new DateTime(2024, 10, 14, 20, 48, 4, 296, DateTimeKind.Utc).AddTicks(8045), null, "Third Post" },
                    { 4, 1, "This is the fourth post.", new DateTime(2024, 10, 14, 20, 48, 4, 296, DateTimeKind.Utc).AddTicks(8048), null, "Fourth Post" },
                    { 5, 1, "This is the fifth post.", new DateTime(2024, 10, 14, 20, 48, 4, 296, DateTimeKind.Utc).AddTicks(8053), null, "Fifth Post" },
                    { 6, 1, "This is the sixth post.", new DateTime(2024, 10, 14, 20, 48, 4, 296, DateTimeKind.Utc).AddTicks(8054), null, "Sixth Post" }
                });

            migrationBuilder.InsertData(
                table: "BlogPostCategories",
                columns: new[] { "BlogPostId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 4, 7 },
                    { 4, 8 },
                    { 5, 9 },
                    { 5, 10 },
                    { 6, 1 },
                    { 6, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_Email",
                table: "users");

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "BlogPostCategories",
                keyColumns: new[] { "BlogPostId", "CategoryId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "blogPosts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "blogPosts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "blogPosts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "blogPosts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "blogPosts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "blogPosts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
