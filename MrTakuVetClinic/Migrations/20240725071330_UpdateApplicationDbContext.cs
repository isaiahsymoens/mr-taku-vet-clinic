using Microsoft.EntityFrameworkCore.Migrations;

namespace MrTakuVetClinic.Migrations
{
    public partial class UpdateApplicationDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Pets_PetId",
                table: "Visit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_VisitType_VisitTypeId",
                table: "Visit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitType",
                table: "VisitType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visit",
                table: "Visit");

            migrationBuilder.RenameTable(
                name: "VisitType",
                newName: "VisitsTypes");

            migrationBuilder.RenameTable(
                name: "Visit",
                newName: "Visits");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_VisitTypeId",
                table: "Visits",
                newName: "IX_Visits_VisitTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_PetId",
                table: "Visits",
                newName: "IX_Visits_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitsTypes",
                table: "VisitsTypes",
                column: "VisitTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visits",
                table: "Visits",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Pets_PetId",
                table: "Visits",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_VisitsTypes_VisitTypeId",
                table: "Visits",
                column: "VisitTypeId",
                principalTable: "VisitsTypes",
                principalColumn: "VisitTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Pets_PetId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_VisitsTypes_VisitTypeId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitsTypes",
                table: "VisitsTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visits",
                table: "Visits");

            migrationBuilder.RenameTable(
                name: "VisitsTypes",
                newName: "VisitType");

            migrationBuilder.RenameTable(
                name: "Visits",
                newName: "Visit");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_VisitTypeId",
                table: "Visit",
                newName: "IX_Visit_VisitTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_PetId",
                table: "Visit",
                newName: "IX_Visit_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitType",
                table: "VisitType",
                column: "VisitTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visit",
                table: "Visit",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Pets_PetId",
                table: "Visit",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_VisitType_VisitTypeId",
                table: "Visit",
                column: "VisitTypeId",
                principalTable: "VisitType",
                principalColumn: "VisitTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
