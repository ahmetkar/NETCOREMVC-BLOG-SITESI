using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHeroAndFeaturedArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("d0568df3-bed6-42c8-bbf6-0f8aaaf54c2a"));

            migrationBuilder.AddColumn<Guid>(
                name: "FeaturedArticle1Id",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FeaturedArticle2Id",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HeroArticleId",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: true);

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
                column: "ConcurrencyStamp",
                value: "f974266c-4494-41c2-8598-0dd01740e5a2");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("1b045307-8a95-4798-bdc1-6ea1126a3af9"));

            migrationBuilder.DropColumn(
                name: "FeaturedArticle1Id",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "FeaturedArticle2Id",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "HeroArticleId",
                table: "SiteSettings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "33ed5302-89e1-4773-97e5-f8b0316797bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "075f6e09-eac5-42c0-b693-85448cffe630");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "9b51be9c-a6d2-4554-b382-42183de25e48");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "560272fe-d32d-48d6-b0fb-3c0cd9d2ecca", "AQAAAAIAAYagAAAAEGmBfKoJoccTct4eoGYjzx/f2elbpaxKDMttwYXK4xnpRKI1uh95qyA6J+ik4CzQkw==", "dd50c70a-e3b3-4d58-9b67-5d113f777d9d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbc4939a-2b4c-4c87-b2a7-874870f16330", "AQAAAAIAAYagAAAAEKd2W4UMgBpjpStViSW0uMNMzl+fh09wxOMIc+yBSwAsD7FcoAicG36HJtGJ4ypX+g==", "fdd43e4e-4c31-4bd5-8efe-2cf1cf56c159" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd026c06-56b0-4c30-9e04-21b6070a6189", "AQAAAAIAAYagAAAAEPGoOxwmx9xgnsXi6pIZxEmZVA0PaQHNHWfYP8xWEO//LpXIOngBpxgYIcj0AL2Y6w==", "929a1878-424a-464d-ac0f-a0084ac44e06" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 19, 5, 52, 980, DateTimeKind.Local).AddTicks(6522));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 19, 5, 52, 980, DateTimeKind.Local).AddTicks(5111));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 19, 5, 52, 980, DateTimeKind.Local).AddTicks(6553));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "Category1Id", "Category2Id", "Category3Id", "Category4Id", "Category5Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SiteTitle", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("d0568df3-bed6-42c8-bbf6-0f8aaaf54c2a"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, null, null, null, null, "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "Blog Web", "https://twitter.com", "https://youtube.com" });
        }
    }
}
