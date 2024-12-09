using Microsoft.EntityFrameworkCore.Migrations;

namespace MrTakuVetClinic.Migrations
{
    public partial class NewDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 1,
                column: "TypeName",
                value: "Administrator");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 2,
                column: "TypeName",
                value: "Pet Owner");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { "administrator@gmail.com", "Administrator", "Administrator", "administrator", "administrator" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 1,
                column: "TypeName",
                value: "Owner");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 2,
                column: "TypeName",
                value: "Doctor");

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "UserTypeId", "TypeName" },
                values: new object[,]
                {
                    { 3, "Pet Owner" },
                    { 4, "Veterinary Assistant" },
                    { 5, "Groomer" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { "admin@gmail.com", "Admin", "Admin", "admin", "admin" });
        }
    }
}
