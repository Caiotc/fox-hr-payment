using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fox_web_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class addinguserrolefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemRole",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemRole",
                table: "Roles");
        }
    }
}
