using System;
using System.Reflection;
using Altkom.ZF.Models;
using Microsoft.EntityFrameworkCore;


namespace Altkom.ZF.DbServices
{
    public static class ScalarFunctionsHelpers
    {
        [DbFunction("ufnGetCountOrder", "dbo")]
        public static int GetCountOrder(int customerId)
        {
            throw new Exception();
        }
    }

    public static class ScalarFunctionsExtentions
    {
        [DbFunction("ufnGetCountOrder", "dbo")]
        public static int GetCountOrder(this Customer customer, int customerId)
        {
        throw new Exception();
        }
    }
}