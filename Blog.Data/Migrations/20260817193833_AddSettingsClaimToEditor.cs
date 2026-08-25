using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSettingsClaimToEditor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("57176022-80e0-4b7a-ab3b-8c9907acbbe1"));

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 5, "Permission", "Settings.View", new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88") });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("d8eefb95-b335-4ea9-93dc-092bbd5e640f"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "54b19488-882b-468d-b6c3-eb39bdda534d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "2e260cbf-acbf-4a32-9429-bd8ef9f74b00");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "7aa0192d-888e-432a-b792-73e4ac540d05");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fb98f7f-e336-4e53-85b1-5ae2fc3322e0", "AQAAAAIAAYagAAAAEFAI22TtU5lTKBtkDg+uBCXdI9yxqMahY4YT3icjD/shn3tI6CKsQ4QE43/VQ3vuAQ==", "02b1db31-1318-4125-b17a-09df57c26206" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e641c8fa-9c00-4ea1-89bd-ad426c87cc14", "AQAAAAIAAYagAAAAENPicpY8OVC+0h/3weiiMiokL5QSQgGPMmjvg3leAp0CCHp4QG7F0H9s/gkA83Y3/g==", "3e2e1572-48a7-4522-953a-f82800f5a983" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1faef02a-6c66-426a-a96e-4597ee55d1a4", "AQAAAAIAAYagAAAAEKxpHUXc89sDO6dLLM31MuS84NmSbyg/nuoWZrf23KcMv8/aanR83+SqVTYCioPh2g==", "e846ce26-8e01-4d19-ae0b-1b7306ace4ee" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 22, 25, 7, 825, DateTimeKind.Local).AddTicks(4977));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 22, 25, 7, 825, DateTimeKind.Local).AddTicks(3738));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 22, 25, 7, 825, DateTimeKind.Local).AddTicks(5010));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "Category1Id", "Category2Id", "Category3Id", "Category4Id", "Category5Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FeaturedArticle1Id", "FeaturedArticle2Id", "FooterDescription", "FooterLogoId", "HeroArticleId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SiteTitle", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("57176022-80e0-4b7a-ab3b-8c9907acbbe1"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, null, null, null, null, "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", null, null, "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "Blog Web", "https://twitter.com", "https://youtube.com" });
        }
    }
}
