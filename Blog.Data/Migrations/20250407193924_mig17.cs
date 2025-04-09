using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FooterArticleSetting",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "FooterCopyright",
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

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"),
                column: "FileType",
                value: "image/png");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FileName", "FileType", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "Sistem", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "images/site-images/tech-footer-logo.png", "image/png", false, null, null },
                    { new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), "Sistem", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "images/site-images/tech-logo.png", "image/png", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FooterDescription", "FooterLogoId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SidebarBottomArticleSetting", "SidebarTopArticleSetting", "TopArticleSetting", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("d9e594ef-989b-4abe-a1cb-ff9d429cea1f"), "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "most-commented", "most-viewed", "last-added", "https://twitter.com", "https://youtube.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("d9e594ef-989b-4abe-a1cb-ff9d429cea1f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"));

            migrationBuilder.AddColumn<string>(
                name: "FooterArticleSetting",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FooterCopyright",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "9b925cde-6cfd-42be-ac58-446f4f7b71c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "2a477d0f-4c80-47d2-b68d-28c531e761bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "b91719c5-fa77-450b-ab70-92e5df1496a6");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"),
                column: "FileType",
                value: "png");
        }
    }
}
