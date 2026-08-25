using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToArticleAndCategory2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("0b3d1c83-57cf-4937-8f9d-1295746e1f77"));

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("12dd1d68-af34-46a5-ae46-7842d39aed20"),
                column: "Slug",
                value: "deneme-makale-1");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("7eb33293-d3fe-4abe-9924-41a64f0138d7"),
                column: "Slug",
                value: "deneme-makale-2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "66ab79e9-ed8f-4c26-a8f2-e7eeaec8fe9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "e9f0a742-ade5-48b0-8212-632aee3e66e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "881135f5-cd00-4c2a-8ede-9563b6f437c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "430e99e2-8c02-41b0-9f29-0fd1d135d7c2", "AQAAAAIAAYagAAAAEF5mIHraq/nZEZugGMxa8uQzAn13ZL1JFzLyhy7ZTQY7YOcMGH0H5n6TGZjebVE+tg==", "40c885bb-c5c0-4e8b-ae6f-8afcabb43cb6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82aae7c5-6e6b-4562-83da-f8635bae00e9", "AQAAAAIAAYagAAAAEPyW5b5B2wtjflUgaGLcBgzkp5ShIB2qtKOCHOu4cZ48hVSoGfKC4wJmFfgeE+oEfA==", "ac44ead4-9bef-4fd5-aae3-fa195aa12558" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "253f24fc-1029-4acb-8820-464da411e318", "AQAAAAIAAYagAAAAEJlYkccAoEFoW27l2tDKURoFoAD/Z+vYs7/1c8mRsco2u4SGfve6U+Hl6rUedh9IYw==", "1a0b9ee7-1efd-4c1a-8e0d-cfa8e84201b4" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("58252b86-ec84-4c93-b3df-e9b1bf39d3bf"),
                column: "Slug",
                value: "aspnet");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80ac9637-35b9-42d1-afb7-297242e1e7c5"),
                column: "Slug",
                value: "visual-studio-2022");

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 18, 7, 58, 525, DateTimeKind.Local).AddTicks(2303));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 18, 7, 58, 525, DateTimeKind.Local).AddTicks(1521));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 18, 7, 58, 525, DateTimeKind.Local).AddTicks(2330));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "SiteTitle", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("672d5918-98f7-467f-8d2b-e495e303a450"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "Blog Web", "last-added", "https://twitter.com", "https://youtube.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("672d5918-98f7-467f-8d2b-e495e303a450"));

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Articles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "631ab60b-d492-4825-a7e9-9add49ddc329");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "ad51cc34-3773-4752-bf7c-12e22871ad7e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "e1b10cec-ebf4-44c3-9b38-8b737dbea510");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4171c6ce-289d-48ea-a1c7-586782083709", "AQAAAAIAAYagAAAAEPsfPvOPH0YY91lzXQSA2mY44plOtKV/xI0uVZFNFPqwrXSDjzjUFTWN6SNbeJhJ1A==", "2b33a356-b920-4d98-8d50-8be8b92fb334" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7c540cd-0a44-48c5-889a-ddb1e7e603ec", "AQAAAAIAAYagAAAAEFiZUdiWSPgDFdpym1ZW9YsCxyCdxvNTlJYK4qEXyyStaS/dNCbYnZ45zdhAi1dpRA==", "8a563996-3a93-4e10-b827-ada05c31ce1e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aeb63c59-6010-42cc-9aa1-b3c853538e43", "AQAAAAIAAYagAAAAEFLvim3a5ugB0OMUnBIvDxgMFYjllEBvARbwmCobqr33uoY+CYbeQI1Vzr+/31I/yA==", "45483245-09ab-4b8c-8f93-fb6b05e15b61" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 17, 42, 51, 78, DateTimeKind.Local).AddTicks(403));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 17, 42, 51, 77, DateTimeKind.Local).AddTicks(9789));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 17, 42, 51, 78, DateTimeKind.Local).AddTicks(409));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "SiteTitle", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("0b3d1c83-57cf-4937-8f9d-1295746e1f77"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "Blog Web", "last-added", "https://twitter.com", "https://youtube.com" });
        }
    }
}
