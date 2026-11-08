namespace Lab1.Deflectors;

public sealed class DeflectorClass2 : Deflector
{
    private const int ProtectionPoints = 30;
    private const int AsteroidHitCost = 3;
    private const int MeteoriteHitCost = 10;

    public DeflectorClass2()
        : base(ProtectionPoints)
    {
    }

    protected override int SmallAsteroidCost => AsteroidHitCost;

    protected override int MeteoriteCost => MeteoriteHitCost;
}
