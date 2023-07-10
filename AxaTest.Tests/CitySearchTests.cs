using AxaTest.DAL;
using FluentAssertions;
using Moq;

namespace AxaTest.Tests;

public class CitySearchTests
{
    private Mock<ICityDAL> _cityDal;
    private CitySearch _citySearch;
    
    [SetUp]
    public void Setup()
    {
        _cityDal = new Mock<ICityDAL>();
        _citySearch = new CitySearch(_cityDal.Object);
    }

    [Test]
    public void Search_WithValidParameter_ShouldReturnExpectedList()
    {
        //arrange
        _cityDal.Setup(x => x.GetCities(It.IsAny<string>())).ReturnsAsync(new List<string> {"BANDUNG", "BANGUI", "BANGKOK", "BANGALORE"});

        //act
        var cities = _citySearch.Search("BANG");
        
        //assert
        cities.NextCities.Count.Should().Be(3);
        cities.NextLetters.Should().Contain("A");
        cities.NextLetters.Should().Contain("U");
        cities.NextLetters.Should().Contain("K");
    }
    
    [TestCase("")]
    [TestCase(null)]
    public void Search_WithInvalidParameter_ShouldThrowsArgumentNullException(string searchString)
    {
        //act
        Action action = () => _citySearch.Search(searchString);
        
        //assert
        Assert.Throws<ArgumentNullException>(() => action());
    }
    
    [Test]
    public void Search_WithSecondValidParameter_ShouldReturnExpectedList()
    {
        //arrange
        _cityDal.Setup(x => x.GetCities(It.IsAny<string>())).ReturnsAsync(new List<string> {"LA PAZ", "LA PLATA", "LAGOS", "LEEDS"});

        //act
        var cities = _citySearch.Search("LA");
        
        //assert
        cities.NextCities.Count.Should().Be(3);
        cities.NextLetters.Should().Contain(" ");
        cities.NextLetters.Should().Contain("G");
    }
    
    [Test]
    public void Search_WithValidParameterButNoMatchingCitiy_ShouldReturnEmptyList()
    {
        //arrange
        _cityDal.Setup(x => x.GetCities(It.IsAny<string>())).ReturnsAsync(new List<string> {"ZARIA", "ZHUGHAI", "ZIBO"});

        //act
        var cities = _citySearch.Search("ZE");
        
        //assert
        cities.NextCities.Count.Should().Be(0);
        cities.NextLetters.Count.Should().Be(0);
    }
}   