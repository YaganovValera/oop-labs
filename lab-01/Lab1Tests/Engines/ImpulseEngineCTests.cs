using Lab1.Engines;

namespace Lab1Tests.Engines;

public class ImpulseEngineCTests
{
    private const int Precision = 8;

    [Fact]
    public void CalculateTravelTime_ReturnsDistanceDividedByConstantSpeed()
    {
        var engine = new ImpulseEngineC();

        double time = engine.CalculateTravelTime(100);

        Assert.Equal(10, time, Precision);
    }

    [Fact]
    public void CalculateFuelConsumption_ReturnsStartupPlusRateTimesTime()
    {
        var engine = new ImpulseEngineC();

        double fuel = engine.CalculateFuelConsumption(100);

        Assert.Equal(25, fuel, Precision);
    }

    [Fact]
    public void CalculateFuelConsumption_IncludesStartupCost()
    {
        var engine = new ImpulseEngineC();
        const double distance = 20;
        double time = engine.CalculateTravelTime(distance);

        double fuel = engine.CalculateFuelConsumption(distance);

        Assert.Equal(2 * time + 5, fuel, Precision);
        Assert.True(fuel > 2 * time);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CalculateTravelTime_RejectsNonPositiveDistance(double distance)
    {
        var engine = new ImpulseEngineC();

        Assert.Throws<ArgumentOutOfRangeException>(() => engine.CalculateTravelTime(distance));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void CalculateFuelConsumption_RejectsNonPositiveDistance(double distance)
    {
        var engine = new ImpulseEngineC();

        Assert.Throws<ArgumentOutOfRangeException>(() => engine.CalculateFuelConsumption(distance));
    }
}
