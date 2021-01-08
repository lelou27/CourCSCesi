using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddCreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM links");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "links",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_links_CreatedBy",
                table: "links",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_links_AspNetUsers_CreatedBy",
                table: "links",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_links_AspNetUsers_CreatedBy",
                table: "links");

            migrationBuilder.DropIndex(
                name: "IX_links_CreatedBy",
                table: "links");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "links");
        }
    }
}
