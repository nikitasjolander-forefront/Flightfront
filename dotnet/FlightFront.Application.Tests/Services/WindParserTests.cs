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

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Direction, Is.EqualTo(330));
            Assert.That(result.Speed, Is.EqualTo(17));
            Assert.That(result.Gust, Is.EqualTo(23));
            Assert.That(result.Unit, Is.EqualTo("KT"));
        });
    }

        public void Test2()
    {
        string metar = "10009KT";

        var result = _sut.TryParse(new string[] { metar }) as Wind;

                Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Direction, Is.EqualTo(100));
            Assert.That(result.Speed, Is.EqualTo(9));
            Assert.That(result.Gust, Is.Null);
            Assert.That(result.Unit, Is.EqualTo("KT"));
        });
    }
}
