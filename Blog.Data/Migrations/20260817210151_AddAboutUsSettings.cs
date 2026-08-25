using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutUsSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("d8eefb95-b335-4ea9-93dc-092bbd5e640f"));

            migrationBuilder.AddColumn<string>(
                name: "AboutUsCard1Description",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUsCard1Title",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUsCard2Description",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUsCard2Title",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUsCard3Description",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUsCard3Title",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUsDescription",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUsTitle",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "77855a14-a825-4ac1-b882-3b5b1a560deb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "54bd053e-40c7-4bb1-8541-fe78ae334616");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "aa6196db-0b74-4cb4-b070-a24b94cb82a7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "438727b3-1506-4a92-978e-cda17e2af510", "AQAAAAIAAYagAAAAEIc362ZmXz+cqPide4gJdSanse0rMS/TCpzCK3HI9lwiyIyXnQ1EEWtRKwwhoqopmw==", "0eeadb6f-4cfe-4393-a85f-366c725e7df0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86487319-9933-4da9-92b7-a8a679e7dd89", "AQAAAAIAAYagAAAAEIXYrglr4osmP/r6pL1NuqfIF1PXZU4DCkXCBw6h0j4xYtcqh3pJpJW1REn4UUfEgA==", "20bf8b7b-ea7d-42b9-828e-60021ac205e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83acafc1-bb91-4d08-96ee-224ca36651bd", "AQAAAAIAAYagAAAAEG/wC30nvxkeWnVPzg8SJejhJXBTLPiufa0aRyuc24ph5KZhBkMMZEHGN8wBa2JsQQ==", "0570032d-a064-4acc-bf42-9b1e779e294c" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 0, 1, 50, 468, DateTimeKind.Local).AddTicks(564));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 0, 1, 50, 468, DateTimeKind.Local).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 0, 1, 50, 468, DateTimeKind.Local).AddTicks(569));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AboutUsCard1Description", "AboutUsCard1Title", "AboutUsCard2Description", "AboutUsCard2Title", "AboutUsCard3Description", "AboutUsCard3Title", "AboutUsDescription", "AboutUsTitle", "AdminPanelLogoId", "Category1Id", "Category2Id", "Category3Id", "Category4Id", "Category5Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FeaturedArticle1Id", "FeaturedArticle2Id", "FooterDescription", "FooterLogoId", "HeroArticleId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SiteTitle", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("c777df55-8c7d-4f45-9bf8-0c4a3a92ece7"), null, null, null, null, null, null, null, null, new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, null, null, null, null, "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", null, null, "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "Blog Web", "https://twitter.com", "https://youtube.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("c777df55-8c7d-4f45-9bf8-0c4a3a92ece7"));

            migrationBuilder.DropColumn(
                name: "AboutUsCard1Description",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "AboutUsCard1Title",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "AboutUsCard2Description",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "AboutUsCard2Title",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "AboutUsCard3Description",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "AboutUsCard3Title",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "AboutUsDescription",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "AboutUsTitle",
                table: "SiteSettings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "8d2d9739-126c-443e-8b82-92649bb1c0fa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "d0b95dbb-2d45-4130-914a-8e90d0b63aa9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "0457eacb-8c55-4f89-b56a-50b226d5c187");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "272769e8-3438-4056-8566-26813782eba6", "AQAAAAIAAYagAAAAEBZyQUw88IMZSR+kMoTSrjKUa7ousmKjiWEdoAixB8pm5LOIg8PXpEtJnq6nmDB85A==", "abca9ca5-9390-46ff-9d01-175adaab767a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2710b909-4440-432d-9a62-cb843847ca9e", "AQAAAAIAAYagAAAAEJV0GDlgBphckLRJSNBMuIBniDPni4raJrYXTuSx4alUhr/235WY1v5EQ/htdSB3HQ==", "664e6ccc-7ed6-40a3-8e52-6b5b45d49595" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81d4aec5-98cc-4406-b939-41af20628564", "AQAAAAIAAYagAAAAEFEvaI6NYqcXqa7WvpBLb9Qr86mHTz8CtaVCI5LMhLH6CZMT/R8hoyHbpm3nR9PLgw==", "d40ca959-82a7-49d5-9376-b0e47ff0c4ca" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 22, 38, 31, 719, DateTimeKind.Local).AddTicks(5875));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 22, 38, 31, 719, DateTimeKind.Local).AddTicks(5269));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 22, 38, 31, 719, DateTimeKind.Local).AddTicks(5882));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "Category1Id", "Category2Id", "Category3Id", "Category4Id", "Category5Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FeaturedArticle1Id", "FeaturedArticle2Id", "FooterDescription", "FooterLogoId", "HeroArticleId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SiteTitle", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("d8eefb95-b335-4ea9-93dc-092bbd5e640f"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, null, null, null, null, "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", null, null, "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "Blog Web", "https://twitter.com", "https://youtube.com" });
        }
    }
}
