namespace Lab1.Engines;

public sealed class JumpEngineGamma : JumpEngine
{
    private const double MaximumDistance = 500;
    private const double QuadraticCoefficient = 0.5;

    public JumpEngineGamma()
        : base(MaximumDistance)
    {
    }

    protected override double ComputeFuelConsumption(double distance)
    {
        return QuadraticCoefficient * distance * distance;
    }
}
