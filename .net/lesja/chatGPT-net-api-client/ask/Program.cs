using System.Text;
using Newtonsoft.Json;

if (args.Length > 0)
{
    var httpClient = new HttpClient();

    httpClient.DefaultRequestHeaders.Add("authorization", "Bearer <INSERT YOUR API KEY HERE>");
    var content = new StringContent(
        "{\"model\": \"text-davinci-001\", \"prompt\": \"" + args[0] + "\",\"temperature\": 1,\"max_tokens\": 100}",
        Encoding.UTF8,
        "application/json"
    );

    var response = await httpClient.PostAsync(
        "https://api.openai.com/v1/completions", 
        content
    );

    var responseString = await response.Content.ReadAsStringAsync();

    try
    {
        var dyData = JsonConvert.DeserializeObject<dynamic>(responseString);
        var guess = GuessCommand(dyData!.choices[0].text);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"--> My guess at the command prompt is: {guess}");
    }
    catch(Exception ex)
    {
        Console.WriteLine($"--> Colud not deserialize the JSON: {ex.Message}");
    }
    finally
    {
        Console.ResetColor();
    }
}
else
{
    Console.WriteLine("--> You need to provide some input");
}

static string GuessCommand(string raw)
{
    Console.WriteLine("--> GPT-3 API Returned Text:");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(raw);

    var lastIndex = raw.LastIndexOf('\n');

    var guess = raw.Substring(lastIndex + 1);

    Console.ResetColor();

    TextCopy.ClipboardService.SetText(guess);

    return guess;
}