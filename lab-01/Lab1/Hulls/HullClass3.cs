namespace Lab1.Hulls;

public sealed class HullClass3 : Hull
{
    private const int ProtectionPoints = 20;
    private const int AsteroidHitCost = 4;
    private const int MeteoriteHitCost = 16;

    public HullClass3()
        : base(ProtectionPoints)
    {
    }

    protected override int SmallAsteroidCost => AsteroidHitCost;

    protected override int? MeteoriteCost => MeteoriteHitCost;
}
