using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("aef7bc96-aea5-41e1-a653-6d9e149ca01e"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "d72a3eb3-86bb-4f4e-874f-5d465740cee8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "0be15394-872c-4829-9f77-5fd6d7f55bc7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "7c64c35a-5d3e-4b2b-a49c-2bd2a24a1adc");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Biography", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImageId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"), 0, "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd", "d65a58c1-b947-43bb-8189-e4a0e8e6ea70", "farukdemir123@gmail.com", true, "Faruk", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), "Demir", false, null, "FARUKDEMIR123@GMAIL.COM", "FARUKDEMIR", "AQAAAAIAAYagAAAAEMyJWJqvzyUBvvZ9S8Gsotehu5oKZeSrLakONwt2BcWJJrUX+FCEKhTD1efPXfYADw==", "+905308132112", false, "ee337b96-90c4-4655-8f7c-95e6c5ace84a", false, "farukdemir" },
                    { new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"), 0, "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd", "333d7116-7243-4832-91d1-3e03bb6a9ba6", "ahmetkar2077@gmail.com", true, "Mehmet", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), "Oz", false, null, "AHMETKAR2077@GMAIL.COM", "MEHMETOZ", "AQAAAAIAAYagAAAAEHTnM5kU4c2JnfWPv6X6KStX/tNg3IILpEbwJ85tS2YXbBRfiesyiILggqf3Gn8qcA==", "+905308142441", false, "f95952c1-5ea7-43bf-a3b6-a75e6fb13e11", false, "mehmetoz" },
                    { new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"), 0, "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd", "5a1f0773-a417-4a62-830f-4c12bc84493d", "ahmetkar2346@gmail.com", true, "Ahmet", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), "Kar", false, null, "AHMETKAR2346@GMAIL.COM", "AHMETKAR", "AQAAAAIAAYagAAAAEOIGH9jcelBBw1F96cdgDsLrhdFUkxF9VmITfjEBbl40bOxnnxYL/y0OrsDzlGHbTA==", "+905308152000", true, "bb4e3241-2709-46c0-931a-e005e64cc47f", false, "ahmetkar" }
                });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 16, 17, 31, 44, 816, DateTimeKind.Local).AddTicks(6786));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 16, 17, 31, 44, 816, DateTimeKind.Local).AddTicks(5930));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 16, 17, 31, 44, 816, DateTimeKind.Local).AddTicks(6799));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "SiteTitle", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("acae8893-24e8-4276-a040-7440328c1be1"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "Blog Web", "last-added", "https://twitter.com", "https://youtube.com" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryID", "CommentCount", "Content", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "Description", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("12dd1d68-af34-46a5-ae46-7842d39aed20"), new Guid("58252b86-ec84-4c93-b3df-e9b1bf39d3bf"), 0, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries..", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), false, null, null, "Deneme makale 1", new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"), 1 },
                    { new Guid("7eb33293-d3fe-4abe-9924-41a64f0138d7"), new Guid("80ac9637-35b9-42d1-afb7-297242e1e7c5"), 0, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries..", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), false, null, null, "Deneme makale 2", new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("12dd1d68-af34-46a5-ae46-7842d39aed20"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("7eb33293-d3fe-4abe-9924-41a64f0138d7"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"));

            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("acae8893-24e8-4276-a040-7440328c1be1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "a0a5c31a-fe7f-4cd3-ace2-62d33babf03c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "e8683e91-6d8a-402e-9a5d-efbcac44617a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "be9b99e0-3999-4798-858d-f8fee682144f");

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 23, 30, 43, 748, DateTimeKind.Local).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 23, 30, 43, 748, DateTimeKind.Local).AddTicks(8606));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 23, 30, 43, 748, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "SiteTitle", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("aef7bc96-aea5-41e1-a653-6d9e149ca01e"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "Blog Web", "last-added", "https://twitter.com", "https://youtube.com" });
        }
    }
}
