using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Insurance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UCR = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ClaimDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LossDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssuredName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    IncurredLoss = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Address1 = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Address2 = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Address3 = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Postcode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InsuranceEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ClaimType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Burglary and Theft" },
                    { 2, "Auto Accident" },
                    { 3, "Fire" }
                });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "AssuredName", "ClaimDate", "Closed", "CompanyId", "IncurredLoss", "LossDate", "UCR" },
                values: new object[] { 1, "Aqib A", new DateTime(2018, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 2500m, new DateTime(2018, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ABCD" });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Active", "Address1", "Address2", "Address3", "Country", "InsuranceEndDate", "Name", "Postcode" },
                values: new object[,]
                {
                    { 1, true, "24 Water Lane", "25 Water Lane", "26 Water Lane", "UK", new DateTime(2023, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aqib LTD", "LS5 5ST" },
                    { 2, false, "34 Fire Lane", "35 Fire Lane", "36 Fire Lane", "Netherlands", new DateTime(2022, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Biqa LTD", "LS7 2ST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "ClaimType");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
