using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("58252b86-ec84-4c93-b3df-e9b1bf39d3bf"), "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, "Asp.net" },
                    { new Guid("80ac9637-35b9-42d1-afb7-297242e1e7c5"), "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, "Visual studio 2022" }
                });

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
                columns: new[] { "Id", "CategoryID", "Content", "CreatedBy", "CreatedDate", "DeleteDate", "DeletedBy", "Description", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("12dd1d68-af34-46a5-ae46-7842d39aed20"), new Guid("58252b86-ec84-4c93-b3df-e9b1bf39d3bf"), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries..", new Guid("17a74024-059f-4747-9849-1ea1613869cc"), false, null, null, "Deneme makale 1", 1 },
                    { new Guid("7eb33293-d3fe-4abe-9924-41a64f0138d7"), new Guid("80ac9637-35b9-42d1-afb7-297242e1e7c5"), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Admin test", new DateTime(2023, 5, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries..", new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"), false, null, null, "Deneme makale 2", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("58252b86-ec84-4c93-b3df-e9b1bf39d3bf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80ac9637-35b9-42d1-afb7-297242e1e7c5"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("17a74024-059f-4747-9849-1ea1613869cc"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("21c11c2f-b0dd-43f8-bf95-1d0c2940fa2d"));
        }
    }
}
