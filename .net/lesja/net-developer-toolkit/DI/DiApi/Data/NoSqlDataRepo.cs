using DiApi.DataServices;

namespace DiApi.Data
{
    public class NoSqlDataRepo : IDataRepo
    {
        //private readonly IDataService _dataService;
        //public NoSqlDataRepo(IDataService dataService)
        // {
        //     _dataService = dataService;
        // }

        private readonly IServiceScopeFactory _scopeFactory;
        public NoSqlDataRepo(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public string GetData()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--> Getting data from No SQL Database");

            using (var scope = _scopeFactory.CreateScope())
            {
                var dataService = scope.ServiceProvider.GetRequiredService<IDataService>();

                dataService.GetProductData("https://something.com/api");

                Console.ResetColor();
                return ("No SQL Data from DB");
            }

            // ctor-di instructions
            //_dataService.GetProductData("https://something.com/api");
            // Console.ResetColor();
            // return ("No SQL Data from DB");
        }

        public string ReturnData() => GetData();
    }
}