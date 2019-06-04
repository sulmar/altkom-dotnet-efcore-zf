using System;
using System.Reflection;
using Altkom.ZF.Models;
using Microsoft.EntityFrameworkCore;

namespace Altkom.ZF.DbServices
{
    public partial class MyContext : DbContext
    {
        public DbQuery<OrderHeader> OrderHeaders { get; set; }
    }
}