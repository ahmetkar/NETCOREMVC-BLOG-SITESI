using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleVisitors",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleVisitors", x => new { x.ArticleId, x.VisitorId });
                    table.ForeignKey(
                        name: "FK_ArticleVisitors_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleVisitors_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "1b3070bf-c367-4a11-8f78-b142490e04c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "0f6f2ddd-e251-4723-9660-d11684367737");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "b376286f-f1a9-4847-8ca2-ccb2b83fdb19");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e35093d-56b3-47d9-a7c1-26729d02cf64", "AQAAAAIAAYagAAAAEEPYxQXRWnnIh0119PtaAUP7QAJVpUyoTL2rov+1zZiFBtv6XvCrJ+IZoB9BH38sKA==", "e045542f-3c3d-43ae-8fc3-a3df3227d20e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "467cb568-285c-4b24-9cb6-2fcef7c4263e", "AQAAAAIAAYagAAAAEJVcbyWB2Zi0slADuY4d7wV6VdnLnEW4pd1g3vUCO7FixTDiUnVUmxlS8MYZmaWtDA==", "7b0c6258-92e2-4a9b-bae5-c057fac1fab9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fac64d41-92d7-4ac1-bb9b-88e3a0e5101f", "AQAAAAIAAYagAAAAEG/Ft1JNtQTl5R1IhAjkohZM7XiBWuK4r2dBdoJmjSt5Dnnuprb3tAhNuJL4TLYZjA==", "5b93a1a4-7694-4f4a-a313-cc2c1bdc7f39" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleVisitors_VisitorId",
                table: "ArticleVisitors",
                column: "VisitorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleVisitors");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "687efd76-e750-4b0f-9bde-ee957143f81e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "8367c1d6-dfa9-468f-94f3-af5ce84509e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "04b1fc74-4b8e-4fb2-ba80-6bdf0e563bb8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a9434e7-3935-452e-9fc4-1645ef7eacc4", "AQAAAAIAAYagAAAAEEzC5X6QIrh8LXN0tAWz0gWfwbjgXwR+WXm7Bx9OSAIznqh68AIzw0haYwWYicw5Jw==", "6f05276b-d769-4214-a9b3-7699bb0be4fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d90dd3b3-efa9-4c2a-a913-1c2cd5bfc757", "AQAAAAIAAYagAAAAEBT4SA/rkK14AM22n0OZEqhaZw/Ls59wuS9Na/NDOjzuQN1SaiUUo6a/fEX/ROli/A==", "5a3aba60-d34f-4ae0-a308-1231c82e62cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "236d3066-087d-4dd1-95c6-606de033aff8", "AQAAAAIAAYagAAAAEEWPrqX4N7D32UN2lpy4VScz9ZWSqE+5vMIi2+H5HO89DI3QNhRjvSni76akSbFBEg==", "7f695572-8522-4a98-bec8-caed3ddc6a64" });
        }
    }
}
