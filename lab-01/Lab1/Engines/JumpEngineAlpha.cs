namespace Lab1.Engines;

public sealed class JumpEngineAlpha : JumpEngine
{
    private const double MaximumDistance = 30;
    private const double FuelPerDistance = 2;

    public JumpEngineAlpha()
        : base(MaximumDistance)
    {
    }

    protected override double ComputeFuelConsumption(double distance)
    {
        return FuelPerDistance * distance;
    }
}
