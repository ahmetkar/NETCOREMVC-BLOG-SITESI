using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("d9e594ef-989b-4abe-a1cb-ff9d429cea1f"));

            migrationBuilder.AddColumn<Guid>(
                name: "AdminPanelLogoId",
                table: "SiteSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "e52e3b64-5983-4aed-a533-6a49e25f7b77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "9d1d7318-b7d3-4133-a7c3-0c12a7f34ed3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "58490202-d9e8-484b-97ec-e2213a25606d");

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("45d40206-31f6-4415-b550-c5b68c644db5"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "last-added", "https://twitter.com", "https://youtube.com" });

            migrationBuilder.CreateIndex(
                name: "IX_SiteSettings_AdminPanelLogoId",
                table: "SiteSettings",
                column: "AdminPanelLogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteSettings_Images_AdminPanelLogoId",
                table: "SiteSettings",
                column: "AdminPanelLogoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteSettings_Images_AdminPanelLogoId",
                table: "SiteSettings");

            migrationBuilder.DropIndex(
                name: "IX_SiteSettings_AdminPanelLogoId",
                table: "SiteSettings");

            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("45d40206-31f6-4415-b550-c5b68c644db5"));

            migrationBuilder.DropColumn(
                name: "AdminPanelLogoId",
                table: "SiteSettings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "fbc1276e-45db-4ee0-84fa-a8df7c6edeae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "19512866-8eb3-498c-a93a-be61aa15689a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "d71927c7-32e7-4090-89b3-60c992a52619");

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("d9e594ef-989b-4abe-a1cb-ff9d429cea1f"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "last-added", "https://twitter.com", "https://youtube.com" });
        }
    }
}
