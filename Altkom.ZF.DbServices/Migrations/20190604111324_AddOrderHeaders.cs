using Microsoft.EntityFrameworkCore.Migrations;

namespace Altkom.ZF.DbServices.Migrations
{
    public partial class AddOrderHeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create or alter view OrderHeaders as 
SELECT o.[Id],
        o.Number,
        o.CustomerId,
        c.FirstName,
        c.LastName,
      [TotalAmount]
      
  FROM [ZFDb].[dbo].[Orders] as o
    inner join [ZFDb].[dbo].[Customers] as c
        on o.CustomerId = c.Id  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop view OrderHeaders");
        }
    }
}
