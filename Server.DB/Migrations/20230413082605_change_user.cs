using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyChat.Server.DB.Migrations
{
    /// <inheritdoc />
    public partial class change_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdentifire",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdentifire",
                table: "AspNetUsers");
        }
    }
}
