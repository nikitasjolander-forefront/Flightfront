using FlightFront.Application.Services;
using FlightFront.Core.Models;

namespace FlightFront.Application.Tests.Services;

public class WindParserTests
{

    private WindParser _sut;

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

        Assert.Pass()

        Assert.Pass(result.Direction == 330 && result.Speed == 17 && result.Gust == 23 && result.Unit == "KT");
    }

        public void Test2()
    {
        string metar = "10009KT";

        var result = _sut.TryParse(new string[] { metar }) as Wind;

        Assert.Pass(result.direction == "100" && result.speed == "09" && result.gust == null && result.unit == "KT");
    }
}
