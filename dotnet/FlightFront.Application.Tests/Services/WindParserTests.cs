using FlightFront.Application.Services;

namespace FlightFront.Application.Tests.Services;

public class WindParserTests
{
    [SetUp]
    public void Setup()
    {
        _sut = new WindParser();
    }

    [Test]
    public void Test1()
    {

        string metar = "33017G23KT";

        var result = _sut.TryParse(new string[] { metar }) as Wind;

        Assert.Pass(result.direction == "330" && result.speed == "17" && result.gust == "23" && result.unit == "KT");
    }

        public void Test1()
    {
        string metar = "10009KT";

        var result = _sut.TryParse(new string[] { metar }) as Wind;

        Assert.Pass(result.direction == "100" && result.speed == "09" && result.gust == null && result.unit == "KT");
    }
}
