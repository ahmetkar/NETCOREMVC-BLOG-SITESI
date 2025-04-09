using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("9ac7cb82-40a3-44b6-ba1f-67988bd6547c"));

            migrationBuilder.AddColumn<string>(
                name: "SiteTitle",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "7a4ef479-5ca7-4642-8774-191b488b7495");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "72a85c00-f32f-4be5-9d57-e1b629a09332");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "3248cfac-1de9-4102-b83b-cd0cc8ed2119");

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "SiteTitle", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("80fb7a3d-662d-48de-baeb-9296f016ad03"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "Blog Web", "last-added", "https://twitter.com", "https://youtube.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("80fb7a3d-662d-48de-baeb-9296f016ad03"));

            migrationBuilder.DropColumn(
                name: "SiteTitle",
                table: "SiteSettings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "dd923b84-4574-4683-b2cb-bbc4f1e12831");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "ccd6c907-d8b1-40a1-a096-62bbf356d7b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "fe7ecffc-1e14-431a-9685-f77d54a28289");

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("9ac7cb82-40a3-44b6-ba1f-67988bd6547c"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "last-added", "https://twitter.com", "https://youtube.com" });
        }
    }
}
