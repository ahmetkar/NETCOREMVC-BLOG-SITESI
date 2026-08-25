using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriberEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("acae8893-24e8-4276-a040-7440328c1be1"));

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("0b3d1c83-57cf-4937-8f9d-1295746e1f77"));

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d65a58c1-b947-43bb-8189-e4a0e8e6ea70", "AQAAAAIAAYagAAAAEMyJWJqvzyUBvvZ9S8Gsotehu5oKZeSrLakONwt2BcWJJrUX+FCEKhTD1efPXfYADw==", "ee337b96-90c4-4655-8f7c-95e6c5ace84a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "333d7116-7243-4832-91d1-3e03bb6a9ba6", "AQAAAAIAAYagAAAAEHTnM5kU4c2JnfWPv6X6KStX/tNg3IILpEbwJ85tS2YXbBRfiesyiILggqf3Gn8qcA==", "f95952c1-5ea7-43bf-a3b6-a75e6fb13e11" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a1f0773-a417-4a62-830f-4c12bc84493d", "AQAAAAIAAYagAAAAEOIGH9jcelBBw1F96cdgDsLrhdFUkxF9VmITfjEBbl40bOxnnxYL/y0OrsDzlGHbTA==", "bb4e3241-2709-46c0-931a-e005e64cc47f" });

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
        }
    }
}
