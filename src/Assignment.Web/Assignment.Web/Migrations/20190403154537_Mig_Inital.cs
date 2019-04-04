using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment.Web.Migrations
{
    public partial class Mig_Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessEntities",
                columns: table => new
                {
                    BusinessId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(maxLength: 150, nullable: true),
                    State = table.Column<string>(maxLength: 150, nullable: true),
                    Zip = table.Column<string>(maxLength: 50, nullable: true),
                    Country = table.Column<string>(maxLength: 150, nullable: true),
                    Mobile = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    ReferredBy = table.Column<string>(maxLength: 50, nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Balance = table.Column<float>(nullable: false),
                    LoginUrl = table.Column<string>(maxLength: 50, nullable: true),
                    SecurityCode = table.Column<string>(maxLength: 50, nullable: true),
                    SMTPServer = table.Column<string>(maxLength: 50, nullable: true),
                    SMTPPort = table.Column<int>(nullable: false),
                    SMTPUsername = table.Column<string>(maxLength: 50, nullable: true),
                    SMTPPassword = table.Column<string>(maxLength: 50, nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(nullable: true),
                    CurrentBalance = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEntities", x => x.BusinessId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessEntities");
        }
    }
}
