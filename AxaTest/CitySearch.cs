using AxaTest.DAL;
using AxaTest.Interfaces;

namespace AxaTest;

public class CitySearch : ICityFinder
{
    private readonly ICityDAL _cityDAL;
    public CitySearch(ICityDAL cityDal)
    {
        _cityDAL = cityDal;
    }
    public ICityResult Search(string searchString)
    {
        if (string.IsNullOrEmpty(searchString)) throw new ArgumentNullException(nameof(searchString));
        
        var cities = GetCities(searchString);
        var listedCities = cities.Result.Where(c => c.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
        
        var nextLetters = (from listedCity in listedCities
            let index = listedCity.IndexOf(searchString, StringComparison.Ordinal)
            select (index < 0)
                ? listedCity
                : listedCity.Remove(index, searchString.Length)
            into cleanPath
            select cleanPath.Substring(0, 1)).ToList();

        return new CityResult
        {
            NextCities = listedCities,
            NextLetters = nextLetters.Distinct().ToList()
        };
    }

    private async Task<List<string?>> GetCities(string searchString)
    {
        return await _cityDAL.GetCities(searchString);
    }
}
