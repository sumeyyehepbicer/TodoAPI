using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.API.Migrations
{
    public partial class _03102022_Assigned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedId",
                table: "Tasks");
        }
    }
}
