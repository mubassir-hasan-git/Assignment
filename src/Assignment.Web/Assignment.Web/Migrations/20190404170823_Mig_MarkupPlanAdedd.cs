using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment.Web.Migrations
{
    public partial class Mig_MarkupPlanAdedd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarkupPlanId",
                table: "BusinessEntities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MarkupPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkupPlan", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntities_MarkupPlanId",
                table: "BusinessEntities",
                column: "MarkupPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEntities_MarkupPlan_MarkupPlanId",
                table: "BusinessEntities",
                column: "MarkupPlanId",
                principalTable: "MarkupPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEntities_MarkupPlan_MarkupPlanId",
                table: "BusinessEntities");

            migrationBuilder.DropTable(
                name: "MarkupPlan");

            migrationBuilder.DropIndex(
                name: "IX_BusinessEntities_MarkupPlanId",
                table: "BusinessEntities");

            migrationBuilder.DropColumn(
                name: "MarkupPlanId",
                table: "BusinessEntities");
        }
    }
}
