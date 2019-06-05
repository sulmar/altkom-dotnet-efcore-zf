using Microsoft.EntityFrameworkCore.Migrations;

namespace Altkom.ZF.DbServices.Migrations
{
    public partial class AddIndexToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FirstName_LastName",
                table: "Customers",
                columns: new[] { "FirstName", "LastName" });

               
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FirstName_LastName",
                table: "Customers");
        }
    }
}
