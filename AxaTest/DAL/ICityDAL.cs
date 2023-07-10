namespace AxaTest.DAL;

public interface ICityDAL
{
    Task<List<string?>> GetCities(string searchString);
}