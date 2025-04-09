using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e4520f6-4144-4b4c-9724-c651046ee24a"),
                column: "ConcurrencyStamp",
                value: "93a4efdc-afa1-4c7a-9875-4dca06f9f481");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b20731ee-51ed-4849-8d24-82d4a998edda"),
                column: "ConcurrencyStamp",
                value: "fe3e14dd-2fcc-43b2-8a63-1d76c8efb7c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dddfbe14-bb8f-4c4f-af4e-c1be34ea9b88"),
                column: "ConcurrencyStamp",
                value: "6550622c-8d62-4f45-bed0-14f449a5b942");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FileName", "FileType", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[] { new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), "Sistem", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "images/defaults/user.png", "png", false, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"));

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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Biography", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImageId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"), 0, "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd", "15cbaa1d-675d-490c-8f8f-2455f9042785", "farukdemir123@gmail.com", true, "Faruk", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), "Demir", false, null, "FARUKDEMIR123@GMAIL.COM", "FARUKDEMIR", "AQAAAAIAAYagAAAAEOp0+bQ75zNgxVX3Lm1s3sZO914d54B5j/az7lh0q3GNDzG8+uwRsAZvIjIjtEFgVg==", "+905308132112", false, "0df500a8-bf2b-4be5-b56c-59eac2510b83", false, "farukdemir" },
                    { new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"), 0, "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd", "613d9edb-bc7e-4188-9364-1ec9257f3465", "ahmetkar2077@gmail.com", true, "Mehmet", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), "Oz", false, null, "AHMETKAR2077@GMAIL.COM", "MEHMETOZ", "AQAAAAIAAYagAAAAEH29IZXuGgXdLO6Jb8NbI/ItVoa3w4O/shqbc+uiTyId87+6NYRToy6P55j7WaF7LA==", "+905308142441", false, "631f617d-7787-4b7c-89b0-67702d2f5953", false, "mehmetoz" },
                    { new Guid("69bffe8d-6794-45d2-bd0f-48599928cdee"), 0, "asdade deneme deneme denem sdfsfs fsdfs fdsfs fsdf sdfsfdsfsdddddddddddddddddddddddddddddfsdfsdfsdfsd", "335b3d22-ff9c-451d-9a75-9d25cc176406", "ahmetkar2346@gmail.com", true, "Ahmet", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), "Kar", false, null, "AHMETKAR2346@GMAIL.COM", "AHMETKAR", "AQAAAAIAAYagAAAAEFnMSa0GFuVrPHYTzc2oJ7+24DKGyC9sikP9le4eVJNbVn1tG87mPPIVpmdJwLs5Sw==", "+905308152000", true, "fd904eb8-81a3-473c-9bfa-1053006e0861", false, "ahmetkar" }
                });
        }
    }
}
