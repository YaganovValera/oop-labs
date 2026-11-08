namespace Lab1.Engines;

public sealed class JumpEngineOmega : JumpEngine
{
    private const double MaximumDistance = 100;

    public JumpEngineOmega()
        : base(MaximumDistance)
    {
    }

    protected override double ComputeFuelConsumption(double distance)
    {
        return distance * Math.Log2(distance + 1);
    }
}
