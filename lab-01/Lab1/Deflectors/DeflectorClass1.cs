namespace Lab1.Deflectors;

public sealed class DeflectorClass1 : Deflector
{
    private const int ProtectionPoints = 2;
    private const int AsteroidHitCost = 1;
    private const int MeteoriteHitCost = 2;

    public DeflectorClass1()
        : base(ProtectionPoints)
    {
    }

    protected override int SmallAsteroidCost => AsteroidHitCost;

    protected override int MeteoriteCost => MeteoriteHitCost;
}
