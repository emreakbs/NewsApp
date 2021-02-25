using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageApp.Data.Migrations
{
    public partial class imagecontent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Images",
                type: "text",
                nullable: true);
        }
    }
}
