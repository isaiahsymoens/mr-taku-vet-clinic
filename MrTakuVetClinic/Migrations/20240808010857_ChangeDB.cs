using Microsoft.EntityFrameworkCore.Migrations;

namespace MrTakuVetClinic.Migrations
{
    public partial class ChangeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PetTypes",
                columns: new[] { "PetTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Dog" },
                    { 2, "Cat" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "UserTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Owner" },
                    { 2, "Doctor" },
                    { 3, "Pet Owner" },
                    { 4, "Veterinary Assistant" },
                    { 5, "Groomer" }
                });

            migrationBuilder.InsertData(
                table: "VisitsTypes",
                columns: new[] { "VisitTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Consultation" },
                    { 2, "Dental care" },
                    { 3, "Grooming" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Active", "Email", "FirstName", "LastName", "MiddleName", "Password", "UserTypeId", "Username" },
                values: new object[] { 1, true, "admin@gmail.com", "Admin", "Admin", null, "admin", 1, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PetTypes",
                keyColumn: "PetTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PetTypes",
                keyColumn: "PetTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VisitsTypes",
                keyColumn: "VisitTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VisitsTypes",
                keyColumn: "VisitTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VisitsTypes",
                keyColumn: "VisitTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 1);
        }
    }
}
