using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogoImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopArticleSetting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SidebarTopArticleSetting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SidebarBottomArticleSetting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterArticleSetting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterCopyright = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterLogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Twitterurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Youtubeurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteSettings_Images_FooterLogoId",
                        column: x => x.FooterLogoId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SiteSettings_Images_LogoImageId",
                        column: x => x.LogoImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "0c90ebc8-71c2-450e-8cce-ab034a89278e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "4065e47d-2cf8-40b0-a3e6-3129d8589722");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "5c6d3854-23e7-4ba0-92af-b8a5deca1cca");

            migrationBuilder.CreateIndex(
                name: "IX_SiteSettings_FooterLogoId",
                table: "SiteSettings",
                column: "FooterLogoId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteSettings_LogoImageId",
                table: "SiteSettings",
                column: "LogoImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "7801da76-1e12-4e67-ae03-ba9a15554819");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "9b48dbf2-c414-4900-956f-9bc002fb4b65");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "740a84c1-0d97-453f-bbd0-955fa16e3490");
        }
    }
}
