using Microsoft.EntityFrameworkCore.Migrations;

namespace MrTakuVetClinic.Migrations
{
    public partial class EstablishOneToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_UserId",
                table: "Pets");
        }
    }
}
