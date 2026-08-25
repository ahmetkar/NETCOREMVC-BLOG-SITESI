using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContactAndAboutUsSectionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("c777df55-8c7d-4f45-9bf8-0c4a3a92ece7"));

            migrationBuilder.AddColumn<string>(
                name: "AboutUsSectionDescription",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUsSectionTitle",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactDescription",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactTitle",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "8bc8d9c5-ad1f-4697-b7cf-49ef78c045c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "9188a76a-bfd6-42fe-888e-747183c44516");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "6ac60843-4ac9-4896-8012-a5b5f8c14ac6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b840be3e-fbcf-4bbe-87c6-c480e452d233", "AQAAAAIAAYagAAAAEE0jIzEPY//b7NdXWFUwiOnrWGdJmfSKE8t6ZnSrlgF6i6C00CTjdbKQwMW44RWWCg==", "c192901a-4370-4494-b08d-a495d16a41c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68ec148e-f965-4eac-bb6e-5d6337c9ebda", "AQAAAAIAAYagAAAAEHNCDpznjmlfHqQiifp+jXW0BNBMg1yVBhLHJXlWSkhjkBdbYb2d2rEoRyinlW34ew==", "7803033e-42b2-48dc-9963-0ba490fe56f7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49e6fa99-cf57-44a2-a4cf-f8a5e1ec7154", "AQAAAAIAAYagAAAAEJ/iu+2aC/mCGfiTusYVmpnp5cgKvSMcIqcdDXu12mNfh+p7vlNyouILQ4qfmf+S0g==", "52f5bed7-7555-4360-b6c0-33adb8067eab" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 0, 57, 52, 555, DateTimeKind.Local).AddTicks(1893));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 0, 57, 52, 555, DateTimeKind.Local).AddTicks(1211));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 0, 57, 52, 555, DateTimeKind.Local).AddTicks(1902));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AboutUsCard1Description", "AboutUsCard1Title", "AboutUsCard2Description", "AboutUsCard2Title", "AboutUsCard3Description", "AboutUsCard3Title", "AboutUsDescription", "AboutUsSectionDescription", "AboutUsSectionTitle", "AboutUsTitle", "AdminPanelLogoId", "Category1Id", "Category2Id", "Category3Id", "Category4Id", "Category5Id", "ContactDescription", "ContactEmail", "ContactTitle", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FeaturedArticle1Id", "FeaturedArticle2Id", "FooterDescription", "FooterLogoId", "HeroArticleId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SiteTitle", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("779664ee-db03-40e5-a195-537151609560"), null, null, null, null, null, null, null, null, null, null, new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, null, null, null, null, null, null, null, "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", null, null, "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "Blog Web", "https://twitter.com", "https://youtube.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: new Guid("779664ee-db03-40e5-a195-537151609560"));

            migrationBuilder.DropColumn(
                name: "AboutUsSectionDescription",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "AboutUsSectionTitle",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "ContactDescription",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "ContactTitle",
                table: "SiteSettings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "77855a14-a825-4ac1-b882-3b5b1a560deb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "54bd053e-40c7-4bb1-8541-fe78ae334616");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "aa6196db-0b74-4cb4-b070-a24b94cb82a7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "438727b3-1506-4a92-978e-cda17e2af510", "AQAAAAIAAYagAAAAEIc362ZmXz+cqPide4gJdSanse0rMS/TCpzCK3HI9lwiyIyXnQ1EEWtRKwwhoqopmw==", "0eeadb6f-4cfe-4393-a85f-366c725e7df0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86487319-9933-4da9-92b7-a8a679e7dd89", "AQAAAAIAAYagAAAAEIXYrglr4osmP/r6pL1NuqfIF1PXZU4DCkXCBw6h0j4xYtcqh3pJpJW1REn4UUfEgA==", "20bf8b7b-ea7d-42b9-828e-60021ac205e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83acafc1-bb91-4d08-96ee-224ca36651bd", "AQAAAAIAAYagAAAAEG/wC30nvxkeWnVPzg8SJejhJXBTLPiufa0aRyuc24ph5KZhBkMMZEHGN8wBa2JsQQ==", "0570032d-a064-4acc-bf42-9b1e779e294c" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("254f012e-4f64-4410-a98d-720e35275c25"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 0, 1, 50, 468, DateTimeKind.Local).AddTicks(564));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a64419c7-c4e0-4501-92a7-42870acb95cf"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 0, 1, 50, 468, DateTimeKind.Local).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: new Guid("a6dcc8a8-0b2d-418b-851f-b30022908a90"),
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 0, 1, 50, 468, DateTimeKind.Local).AddTicks(569));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AboutUsCard1Description", "AboutUsCard1Title", "AboutUsCard2Description", "AboutUsCard2Title", "AboutUsCard3Description", "AboutUsCard3Title", "AboutUsDescription", "AboutUsTitle", "AdminPanelLogoId", "Category1Id", "Category2Id", "Category3Id", "Category4Id", "Category5Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FacebookUrl", "FeaturedArticle1Id", "FeaturedArticle2Id", "FooterDescription", "FooterLogoId", "HeroArticleId", "InstagramUrl", "IsAIEnabled", "IsDeleted", "LogoImageId", "ModifiedBy", "ModifiedDate", "SiteTitle", "Twitterurl", "Youtubeurl" },
                values: new object[] { new Guid("c777df55-8c7d-4f45-9bf8-0c4a3a92ece7"), null, null, null, null, null, null, null, null, new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, null, null, null, null, "deneme@gmail.com", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://facebook.com/", null, null, "Techblog is a blog site.", new Guid("57597e1f-9547-423c-9485-ad7a390bd5a6"), null, "https://instagram.com/", true, false, new Guid("e30e3542-ca2a-4f87-8366-65fd3e287a7d"), null, null, "Blog Web", "https://twitter.com", "https://youtube.com" });
        }
    }
}
