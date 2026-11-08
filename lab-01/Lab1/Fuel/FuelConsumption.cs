namespace Lab1.Fuel;

public readonly struct FuelConsumption
{
    public FuelConsumption(double activePlasma, double gravitonMatter)
    {
        if (activePlasma < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(activePlasma), activePlasma, "Fuel amount must be non-negative.");
        }

        if (gravitonMatter < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(gravitonMatter), gravitonMatter, "Fuel amount must be non-negative.");
        }

        ActivePlasma = activePlasma;
        GravitonMatter = gravitonMatter;
    }

    public double ActivePlasma { get; }

    public double GravitonMatter { get; }

    public static FuelConsumption None => new(0, 0);

    public static FuelConsumption operator +(FuelConsumption left, FuelConsumption right)
    {
        return new FuelConsumption(left.ActivePlasma + right.ActivePlasma, left.GravitonMatter + right.GravitonMatter);
    }
}
