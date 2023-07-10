// See https://aka.ms/new-console-template for more information

using AxaTest;
using AxaTest.DAL;
using Microsoft.Extensions.Configuration;

class Program
{
    private static IConfiguration _iconfiguration;

    static void Main(string[] args)
    {
        GetAppSettingsFile();
        var cityDAL = new CityDAL(_iconfiguration);
        var citySearch = new CitySearch(cityDAL);
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input)) return;
        var result = citySearch.Search(input);
        foreach (var city in result.NextCities)
        {
            Console.WriteLine(city);
        }

        foreach (var letter in result.NextLetters)
        {
            Console.WriteLine(letter);
        }
    }

    static void GetAppSettingsFile()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _iconfiguration = builder.Build();
    }
}