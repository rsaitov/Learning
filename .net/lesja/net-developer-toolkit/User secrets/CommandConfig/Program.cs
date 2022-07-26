using Microsoft.Extensions.Configuration;

IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets(typeof(Program).Assembly, optional: true);

IConfigurationRoot config = builder.Build();

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("The password is... " + config["Password"]);
Console.ResetColor();
