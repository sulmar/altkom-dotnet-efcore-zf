using Microsoft.EntityFrameworkCore.Migrations;

namespace Altkom.ZF.DbServices.Migrations
{
    public partial class AddufnGetCountOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create function dbo.ufnGetCountOrder(@customerId int)
  returns INT
  as 
  BEGIN
    declare @count INT
    select @count = count(*) from dbo.Orders where CustomerId = @customerId
    RETURN @count
  END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop function dbo.ufnGetCountOrder");
        }
    }
}
