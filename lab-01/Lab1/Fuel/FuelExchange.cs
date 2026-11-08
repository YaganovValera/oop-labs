namespace Lab1.Fuel;

public sealed class FuelExchange
{
    public FuelExchange(double activePlasmaPrice, double gravitonMatterPrice)
    {
        if (activePlasmaPrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(activePlasmaPrice), activePlasmaPrice, "Price must be non-negative.");
        }

        if (gravitonMatterPrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(gravitonMatterPrice), gravitonMatterPrice, "Price must be non-negative.");
        }

        ActivePlasmaPrice = activePlasmaPrice;
        GravitonMatterPrice = gravitonMatterPrice;
    }

    public double ActivePlasmaPrice { get; }

    public double GravitonMatterPrice { get; }

    public double CalculateCost(FuelConsumption consumption)
    {
        return consumption.ActivePlasma * ActivePlasmaPrice + consumption.GravitonMatter * GravitonMatterPrice;
    }
}
