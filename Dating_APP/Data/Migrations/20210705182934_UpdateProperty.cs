using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating_APP.Data.Migrations
{
    public partial class UpdateProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Photos",
                newName: "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Photos",
                newName: "ImageUrl");
        }
    }
}
