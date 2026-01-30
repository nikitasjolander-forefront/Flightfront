using FlightFront.Infrastructure.Services;

namespace FlightFront.Infrastructure.Tests.Services;

[TestFixture]
public class AirportSearchServiceTests
{
    private const string TestCsvPath = "TestData/airports.csv";
    private AirportSearchService _sut = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var testDataDir = Path.GetDirectoryName(TestCsvPath)!;
        if (!Directory.Exists(testDataDir))
            Directory.CreateDirectory(testDataDir);

        var csvContent = """
            type,name,continent,municipality,icao_code
            large_airport,Stockholm Arlanda Airport,EU,Stockholm,ESSA
            medium_airport,Bromma Stockholm Airport,EU,Stockholm,ESSB
            large_airport,London Heathrow Airport,EU,London,EGLL
            small_airport,Small Regional,EU,SmallTown,XXXX
            """;

        File.WriteAllText(TestCsvPath, csvContent);

        _sut = new AirportSearchService(TestCsvPath);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (File.Exists(TestCsvPath))
            File.Delete(TestCsvPath);

        var testDataDir = Path.GetDirectoryName(TestCsvPath);
        if (Directory.Exists(testDataDir))
            Directory.Delete(testDataDir);
    }

    [Test]
    public async Task SearchIcaoAsync_WithMatchingCode_ReturnsAirport()
    {
        // Act
        var results = await _sut.SearchIcaoAsync("ESSA");

        // Assert
        Assert.That(results, Has.Exactly(1).Items);
        Assert.That(results.First().IcaoCode, Is.EqualTo("ESSA"));
    }

    [Test]
    public async Task SearchIcaoAsync_WithPartialCode_ReturnsMatchingAirports()
    {
        // Act
        var results = await _sut.SearchIcaoAsync("ESS");

        // Assert
        Assert.That(results.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task SearchIcaoAsync_IsCaseInsensitive()
    {
        // Act
        var results = await _sut.SearchIcaoAsync("essa");

        // Assert
        Assert.That(results, Has.Exactly(1).Items);
    }

    [Test]
    public async Task SearchIcaoAsync_WithNoMatch_ReturnsEmpty()
    {
        // Act
        var results = await _sut.SearchIcaoAsync("ZZZZ");

        // Assert
        Assert.That(results, Is.Empty);
    }

    [Test]
    public async Task SearchNameAsync_WithMatchingName_ReturnsAirports()
    {
        // Act
        var results = await _sut.SearchNameAsync("Arlanda");

        // Assert
        Assert.That(results, Has.Exactly(1).Items);
        Assert.That(results.First().Name, Does.Contain("Arlanda"));
    }

    [Test]
    public async Task SearchNameAsync_IsCaseInsensitive()
    {
        // Act
        var results = await _sut.SearchNameAsync("heathrow");

        // Assert
        Assert.That(results, Has.Exactly(1).Items);
    }

    [Test]
    public async Task SearchMuncipalityAsync_WithMatchingCity_ReturnsAirports()
    {
        // Act
        var results = await _sut.SearchMuncipalityAsync("Stockholm");

        // Assert
        Assert.That(results.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task SearchMuncipalityAsync_IsCaseInsensitive()
    {
        // Act
        var results = await _sut.SearchMuncipalityAsync("london");

        // Assert
        Assert.That(results, Has.Exactly(1).Items);
    }

    [Test]
    public async Task LoadAirports_FiltersOutSmallAirports()
    {
        // The test CSV has a small_airport that should be filtered out
        // Act
        var icaoResults = await _sut.SearchIcaoAsync("XXXX");

        // Assert
        Assert.That(icaoResults, Is.Empty, "Small airports should be filtered out");
    }

    [Test]
    public async Task LoadAirports_IncludesLargeAndMediumAirports()
    {
        // Act
        var allStockholm = await _sut.SearchMuncipalityAsync("Stockholm");

        // Assert - should include both large (ESSA) and medium (ESSB) airports
        Assert.That(allStockholm.Count(), Is.EqualTo(2));
    }
}