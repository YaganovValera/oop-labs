using Lab1.Fuel;

namespace Lab1Tests.Fuel;

public class FuelExchangeTests
{
    private const int Precision = 8;

    [Fact]
    public void CalculateCost_UsesSeparatePricesForEachFuel()
    {
        var exchange = new FuelExchange(activePlasmaPrice: 2, gravitonMatterPrice: 5);
        var consumption = new FuelConsumption(activePlasma: 3, gravitonMatter: 4);

        Assert.Equal(26, exchange.CalculateCost(consumption), Precision);
    }

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void Constructor_RejectsNegativePrice(double activePlasmaPrice, double gravitonMatterPrice)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new FuelExchange(activePlasmaPrice, gravitonMatterPrice));
    }

    [Theory]
    [InlineData(-0.1, 0)]
    [InlineData(0, -1)]
    public void FuelConsumption_RejectsNegativeAmount(double activePlasma, double gravitonMatter)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new FuelConsumption(activePlasma, gravitonMatter));
    }

    [Fact]
    public void FuelConsumption_NoneAndAddition_StayNonNegative()
    {
        FuelConsumption sum = FuelConsumption.None + new FuelConsumption(1.5, 2.5);

        Assert.Equal(1.5, sum.ActivePlasma);
        Assert.Equal(2.5, sum.GravitonMatter);
    }
}
