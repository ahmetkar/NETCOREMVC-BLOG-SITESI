using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isMenuCategory",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPrimeCategory",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("58252b86-ec84-4c93-b3df-e9b1bf39d3bf"),
                columns: new[] { "isMenuCategory", "isPrimeCategory" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80ac9637-35b9-42d1-afb7-297242e1e7c5"),
                columns: new[] { "isMenuCategory", "isPrimeCategory" },
                values: new object[] { false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isMenuCategory",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "isPrimeCategory",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "4a021802-df3c-4ce8-a9d6-72953370fa89");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "49d9830e-b811-4a39-8633-c2db6d56ec9f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "157ce315-795f-48bb-bd91-e55a8e040b5e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c62dc340-73e6-4566-864e-fff076268c43", "AQAAAAIAAYagAAAAECT+j9mPmjz86gQunfB3oswkIokJs9rO0+Gu22AaJNbHc06y6HgpyYAuM7ZHKOb7kA==", "d35a5d41-dce3-45c4-898b-200cd8992319" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f57ff1ba-5146-46d4-b1cb-3ddf7e4196e7", "AQAAAAIAAYagAAAAENfDVxETe2YKPAUoiUw5UBsAijWBW4rVRS3Go5BpOUpdxD/Ww0MqybvtkDoUmSv6lw==", "f26a6588-4d8c-41e6-82b2-a91a9a9b3567" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f66967a6-5265-4f9f-8975-ddb719639033", "AQAAAAIAAYagAAAAEA9e2xi7nC02F/1LYzPnq6DuZMa9340noRgExaiHNFrkF3BIhy3RJcOCKnCTplcXNw==", "56583d9a-b3a2-4ac6-8823-7243d3344d42" });
        }
    }
}
