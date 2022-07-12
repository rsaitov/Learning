namespace DiApi.Data
{
    public class NoSqlDataRepo : IDataRepo
    {
        public string GetData()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("--> Getting data from No SQL Database");
            Console.ResetColor();

            return("No SQL Data from DB");
        }

        public string ReturnData() => GetData();
    }
}