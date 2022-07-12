using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiApi.Data
{
    public class SqlDataRepo : IDataRepo
    {
        public string ReturnData()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("--> Getting data from SQL Database");
            Console.ResetColor();

            return("SQL Data from DB");
        }
    }
}