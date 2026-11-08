using Lab1.Engines;

namespace Lab1Tests.Engines;

public class ImpulseEngineETests
{
    private const int Precision = 8;
    private const double InitialSpeed = 5;
    private const double Acceleration = 0.1;
    private const double FuelConsumptionRate = 8;
    private const double StartupFuel = 15;

    [Fact]
    public void CalculateTravelTime_UsesExponentialSpeedFormula()
    {
        var engine = new ImpulseEngineE();
        const double distance = 100;
        double expectedTime = Math.Log(1 + distance * Acceleration / InitialSpeed) / Acceleration;

        double time = engine.CalculateTravelTime(distance);

        Assert.Equal(expectedTime, time, Precision);
    }

    [Fact]
    public void CalculateFuelConsumption_ReturnsStartupPlusRateTimesTime()
    {
        var engine = new ImpulseEngineE();
        const double distance = 100;
        double time = Math.Log(1 + distance * Acceleration / InitialSpeed) / Acceleration;

        double fuel = engine.CalculateFuelConsumption(distance);

        Assert.Equal(StartupFuel + FuelConsumptionRate * time, fuel, Precision);
    }

    [Fact]
    public void OnLongDistance_TravelsFasterAndConsumesMoreFuelThanClassC()
    {
        const double distance = 1000;
        var engineC = new ImpulseEngineC();
        var engineE = new ImpulseEngineE();

        Assert.True(engineE.CalculateTravelTime(distance) < engineC.CalculateTravelTime(distance));
        Assert.True(engineE.CalculateFuelConsumption(distance) > engineC.CalculateFuelConsumption(distance));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CalculateTravelTime_RejectsNonPositiveDistance(double distance)
    {
        var engine = new ImpulseEngineE();

        Assert.Throws<ArgumentOutOfRangeException>(() => engine.CalculateTravelTime(distance));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void CalculateFuelConsumption_RejectsNonPositiveDistance(double distance)
    {
        var engine = new ImpulseEngineE();

        Assert.Throws<ArgumentOutOfRangeException>(() => engine.CalculateFuelConsumption(distance));
    }
}
