using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fox_web_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class userphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPhoto",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPhoto",
                table: "Users");
        }
    }
}
