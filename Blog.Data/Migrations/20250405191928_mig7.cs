using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("12dd1d68-af34-46a5-ae46-7842d39aed20"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("7eb33293-d3fe-4abe-9924-41a64f0138d7"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("17a74024-059f-4747-9849-1ea1613869cc"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "FileName", "FileType", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("17a74024-059f-4747-9849-1ea1613869cc"), "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "images/deneme", "png", false, null, null },
                    { new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "images/deneme2", "png", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryID", "Content", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "Description", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("12dd1d68-af34-46a5-ae46-7842d39aed20"), new Guid("58252b86-ec84-4c93-b3df-e9b1bf39d3bf"), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries..", new Guid("17a74024-059f-4747-9849-1ea1613869cc"), false, null, null, "Deneme makale 1", new Guid("070f54e2-bf16-4d50-bd52-a7f9cd87c479"), 1 },
                    { new Guid("7eb33293-d3fe-4abe-9924-41a64f0138d7"), new Guid("80ac9637-35b9-42d1-afb7-297242e1e7c5"), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries..", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), false, null, null, "Deneme makale 2", new Guid("258794ef-c8f5-4ab5-8818-bcfd3218036a"), 1 }
                });
        }
    }
}
