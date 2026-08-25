using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleClaimsForPolicies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("1b045307-8a95-4798-bdc1-6ea1126a3af9"));

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permission", "Categories.View", new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a") },
                    { 2, "Permission", "Users.View", new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a") },
                    { 3, "Permission", "Settings.View", new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a") },
                    { 4, "Permission", "Categories.View", new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88") }
                });

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
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7aa0192d-888e-432a-b792-73e4ac540d05", "Editor", "EDITOR" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("57176022-80e0-4b7a-ab3b-8c9907acbbe1"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "3bb17412-5f85-46b4-b698-cedab170982b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "56fc6a0d-ee9f-4f48-8572-5051b5c0cd36");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f974266c-4494-41c2-8598-0dd01740e5a2", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3664a80-9882-43e4-9871-c5f28aa9b69f", "AQAAAAIAAYagAAAAELFCq3y6jlDXFWeKCaLYTI3gE2VSNhiahHqZexg1nLY62xIxbkT9//0tQ3l78wXDZA==", "6c784cb6-50db-450c-ab8e-50471d313ccc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3f82d4c-1f77-407e-b734-62f60cca2648", "AQAAAAIAAYagAAAAEBPc3GnDM2W1n4nN0YR/gLoaO19DtGbDxB7NJOvtispD9C7/OIUNeaAklFuAwFd0fQ==", "864b0b08-8147-4ca7-aa44-9d5712ca84de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6cfbce5f-54c9-43d2-83ff-16cace7b28ef", "AQAAAAIAAYagAAAAENGoui/k/P3JA3KGwlXZVJ4AhcpEfUR65qt+dLsSqzSV3IE/a4htL76fGfaLAk8R6Q==", "f86674d7-74c6-467d-8d90-9b7435ad9f35" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 21, 14, 3, 725, DateTimeKind.Local).AddTicks(7442));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 21, 14, 3, 725, DateTimeKind.Local).AddTicks(6666));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 21, 14, 3, 725, DateTimeKind.Local).AddTicks(7452));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "Category1Id", "Category2Id", "Category3Id", "Category4Id", "Category5Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FeaturedArticle1Id", "FeaturedArticle2Id", "FooterDescription", "FooterLogoId", "HeroArticleId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SiteTitle", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("1b045307-8a95-4798-bdc1-6ea1126a3af9"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, null, null, null, null, "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", null, null, "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "Blog Web", "https://twitter.com", "https://youtube.com" });
        }
    }
}
