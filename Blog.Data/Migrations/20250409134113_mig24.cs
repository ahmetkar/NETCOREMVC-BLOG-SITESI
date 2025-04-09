using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("d3b85983-d42b-4804-9003-fbe35d80383e"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Pages");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "9cf52fc0-423f-4b5f-a77a-397894059bf2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "9a9c82d0-0915-4e01-9247-9b7bbd4dc7f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "9dee0529-4381-4ad3-873d-ead8f2a7cc6b");

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 16, 41, 12, 160, DateTimeKind.Local).AddTicks(5671));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 16, 41, 12, 160, DateTimeKind.Local).AddTicks(5159));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 16, 41, 12, 160, DateTimeKind.Local).AddTicks(5677));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "SiteTitle", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("89345353-2d46-48e4-be51-31a05335fc35"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "Blog Web", "last-added", "https://twitter.com", "https://youtube.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("89345353-2d46-48e4-be51-31a05335fc35"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Pages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "db92acc3-7923-43dd-bd01-e00dcc17b01c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "337f4eff-8856-438c-b198-069bcc26a410");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "7ee7b588-de98-445a-a320-46cdbb1c98ac");

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                columns: new[] { "CreatedDate", "IsActive" },
                values: new object[] { new DateTime(2025, 4, 9, 15, 37, 34, 113, DateTimeKind.Local).AddTicks(2972), true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                columns: new[] { "CreatedDate", "IsActive" },
                values: new object[] { new DateTime(2025, 4, 9, 15, 37, 34, 113, DateTimeKind.Local).AddTicks(2044), true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                columns: new[] { "CreatedDate", "IsActive" },
                values: new object[] { new DateTime(2025, 4, 9, 15, 37, 34, 113, DateTimeKind.Local).AddTicks(2979), true });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "SiteTitle", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("d3b85983-d42b-4804-9003-fbe35d80383e"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "Blog Web", "last-added", "https://twitter.com", "https://youtube.com" });
        }
    }
}
