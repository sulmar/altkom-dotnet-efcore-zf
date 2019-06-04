using Microsoft.EntityFrameworkCore.Migrations;

namespace Altkom.ZF.DbServices.Migrations
{
    public partial class AddSalaryToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);


            migrationBuilder.Sql("update dbo.Customers set Salary = 0 where Salary is null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Customers");
        }
    }
}
