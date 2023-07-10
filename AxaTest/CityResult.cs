using AxaTest.Interfaces;

namespace AxaTest;

public class CityResult : ICityResult
{
    public ICollection<string> NextLetters { get; set; }
    public ICollection<string?> NextCities { get; set; }
}