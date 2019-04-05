using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment.Web.Migrations
{
    public partial class Mig_SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MarkupPlan",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "HT Agent and Transaction Above 15 Lac" });

            migrationBuilder.InsertData(
                table: "MarkupPlan",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "HT Agent and Transaction Above 35 Lac" });

            migrationBuilder.InsertData(
                table: "MarkupPlan",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "HT Agent and Transaction Above 45 Lac" });

            migrationBuilder.InsertData(
                table: "BusinessEntities",
                columns: new[] { "BusinessId", "Balance", "City", "Code", "ContactPerson", "Country", "CreatedOnUtc", "CurrentBalance", "Deleted", "Email", "LoginUrl", "Logo", "MarkupPlanId", "Mobile", "Name", "Phone", "ReferredBy", "SMTPPassword", "SMTPPort", "SMTPServer", "SMTPUsername", "SecurityCode", "State", "Status", "Street", "UpdatedOnUtc", "Zip" },
                values: new object[,]
                {
                    { 1, 0f, "Dhaka", "H1213", null, "Bangladesh", new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), 0f, false, "test@gmail.com", null, "default.png", 1, "01455654255", "Eco Travels", null, null, null, 0, null, null, null, "Dhaka", 1, null, new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), "1522" },
                    { 2, 0f, "Dhaka", "1213", null, "Bangladesh", new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), 0f, false, "nz@gmail.com", null, "default.png", 1, "01455654255", "Nazma Travels", null, null, null, 0, null, null, null, "Dhaka", 1, null, new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), "1522" },
                    { 3, 0f, "Dhaka", "F4555", null, "Bangladesh", new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), 0f, false, "desh@gmail.com", null, "default.png", 1, "01455654255", "Desh Travels", null, null, null, 0, null, null, null, "Dhaka", 1, null, new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), "1522" },
                    { 5, 0f, "Dhaka", "G343422", null, "Bangladesh", new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), 0f, false, "test@gmail.com", null, "default.png", 1, "01455654255", "Undefined Travels", null, null, null, 0, null, null, null, "Dhaka", 1, null, new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), "1522" },
                    { 4, 0f, "Dhaka", "H1213", null, "Bangladesh", new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), 0f, false, "test@gmail.com", null, "default.png", 2, "01455654255", "Alone Travels", null, null, null, 0, null, null, null, "Dhaka", 1, null, new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), "1522" },
                    { 6, 0f, "Dhaka", "23552", null, "Bangladesh", new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), 0f, false, "test@gmail.com", null, "default.png", 3, "01455654255", "No Name Travels", null, null, null, 0, null, null, null, "Dhaka", 1, null, new DateTime(2019, 4, 5, 12, 1, 51, 678, DateTimeKind.Utc), "1522" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessEntities",
                keyColumn: "BusinessId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BusinessEntities",
                keyColumn: "BusinessId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BusinessEntities",
                keyColumn: "BusinessId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BusinessEntities",
                keyColumn: "BusinessId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BusinessEntities",
                keyColumn: "BusinessId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BusinessEntities",
                keyColumn: "BusinessId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MarkupPlan",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MarkupPlan",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MarkupPlan",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
