using Microsoft.EntityFrameworkCore.Migrations;

namespace MealOrdering.Server.Data.Migrations
{
    public partial class A001InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "public",
                table: "user",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "public",
                table: "user");
        }
    }
}
