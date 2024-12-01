namespace AdventOfCode2024;

internal static class InputFetcher
{
    public static async Task<Input> Fetch<TDay>() where TDay : ISolution
    {
        var inputsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input Files");
        var day = typeof(TDay).Namespace!.Split('.').Last();
        var inputFilePath = Path.Combine(inputsFolderPath, $"{day}.txt");

        if (File.Exists(inputFilePath))
        {
            return new Input(await File.ReadAllTextAsync(inputFilePath));
        }

        var sessionCookieFilePath = Path.Combine(inputsFolderPath, "Session Cookie.txt");
        if (!File.Exists(sessionCookieFilePath))
        {
            Directory.CreateDirectory(inputsFolderPath);
            File.Create(sessionCookieFilePath).Close();
            throw new Exception($"Missing session cookie.");
        }

        var sessionCookie = await File.ReadAllTextAsync(sessionCookieFilePath);

        var dayNumber = day[^2..].TrimStart('0');
        var inputText = await FetchFromWebsite($"https://adventofcode.com/2024/day/{dayNumber}/input", sessionCookie);

        await File.WriteAllTextAsync(inputFilePath, inputText);

        return new Input(inputText);
    }

    private static async Task<string> FetchFromWebsite(string url, string sessionCookie)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Cookie", sessionCookie);

        var response = await httpClient.GetAsync(url);
        var responseContent = (await response.Content.ReadAsStringAsync()).TrimEnd('\n');

        if (!response.IsSuccessStatusCode)
        {
            if (responseContent == "Puzzle inputs differ by user.  Please log in to get your puzzle input.")
            {
                throw new ArgumentException("Invalid session cookie.", nameof(sessionCookie));
            }

            throw new Exception($"Unknown error getting input from website: {responseContent}");
        }

        return responseContent;
    }
}
