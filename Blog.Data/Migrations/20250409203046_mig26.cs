using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("dd16e32c-c231-4a87-a141-bb685a2ed2dd"));

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
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"),
                column: "FileName",
                value: "defaults/user.png");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"),
                column: "FileName",
                value: "site-images/tech-footer-logo.png");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"),
                column: "FileName",
                value: "site-images/tech-logo.png");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: "20cc900f-b926-4dce-b875-88d4b076b2e9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "08ea6b2f-43ee-4241-a42f-c63003bdf96f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "3a01c20e-a07d-4f56-8bc5-837e8b2ecec9");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"),
                column: "FileName",
                value: "images/defaults/user.png");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"),
                column: "FileName",
                value: "images/site-images/tech-footer-logo.png");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"),
                column: "FileName",
                value: "images/site-images/tech-logo.png");

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 20, 46, 52, 772, DateTimeKind.Local).AddTicks(3221));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 20, 46, 52, 772, DateTimeKind.Local).AddTicks(2654));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 20, 46, 52, 772, DateTimeKind.Local).AddTicks(3226));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AdminPanelLogoId", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "SiteTitle", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("dd16e32c-c231-4a87-a141-bb685a2ed2dd"), new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "Blog Web", "last-added", "https://twitter.com", "https://youtube.com" });
        }
    }
}
