using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTags",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => new { x.ArticleId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ArticleTags_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "d8cc0dad-3ac1-4829-9073-9a52fbc101f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "2f6663d5-243b-42b6-b6c3-f52aa068deb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "f38b1996-9562-4579-8749-4631ec6d9325");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "Biography", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd", "15cbaa1d-675d-490c-8f8f-2455f9042785", "AQAAAAIAAYagAAAAEOp0+bQ75zNgxVX3Lm1s3sZO914d54B5j/az7lh0q3GNDzG8+uwRsAZvIjIjtEFgVg==", "0df500a8-bf2b-4be5-b56c-59eac2510b83" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "Biography", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd", "613d9edb-bc7e-4188-9364-1ec9257f3465", "AQAAAAIAAYagAAAAEH29IZXuGgXdLO6Jb8NbI/ItVoa3w4O/shqbc+uiTyId87+6NYRToy6P55j7WaF7LA==", "631f617d-7787-4b7c-89b0-67702d2f5953" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "Biography", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd", "335b3d22-ff9c-451d-9a75-9d25cc176406", "AQAAAAIAAYagAAAAEFnMSa0GFuVrPHYTzc2oJ7+24DKGyC9sikP9le4eVJNbVn1tG87mPPIVpmdJwLs5Sw==", "fd904eb8-81a3-473c-9bfa-1053006e0861" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_TagId",
                table: "ArticleTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "87d3a0e2-a4bb-4364-b63c-4e251279fb61");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "08fc37e0-ab8e-4adf-82a7-70f3d9da1152");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "4d0a82ef-7690-4755-90d1-12f4e5f2272f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7c20caf-8761-4832-bf32-b9319ab7d869", "AQAAAAIAAYagAAAAENTIi14Rj5GxtviXv8drWbMqIj0l/dyfb/x6Ta24EhtiEiMZVFQMdJCB+YRrbjq0AQ==", "5135e3b7-33f7-45c1-adfa-f5a1f1e5cae8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b995ecb7-7101-43b5-8dcb-0b74c5b4f26d", "AQAAAAIAAYagAAAAEFc2fhrOzIkstEdYMCQ13B5jB/d1oUKxNQtpcV2MaoWmM9sPlVV0fx4rmpnOcu+daw==", "95e20b43-adb3-432e-a3d9-0c33b828b105" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79a4190a-9999-4c1e-bd4a-69aedf71b3f2", "AQAAAAIAAYagAAAAEFVi3PNKchxluxtCUsrLz4UnYVaxz9T8utNtCcei8c4ME3D+vXKW1JHqCMa7na5nkg==", "7a53adca-a8b6-405f-90ee-0d66e14d654d" });
        }
    }
}
