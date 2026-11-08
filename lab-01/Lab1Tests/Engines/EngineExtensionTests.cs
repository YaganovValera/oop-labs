using Lab1.Engines;

namespace Lab1Tests.Engines;

public class EngineExtensionTests
{
    private const int Precision = 8;

    [Fact]
    public void NitrineNebula_PenalizesBothImpulseEngines_AndKeepsClassECheaper()
    {
        var engineC = new ImpulseEngineC();
        var engineE = new ImpulseEngineE();
        const double distance = 100;

        Assert.True(engineC.NitrineTimeMultiplier > 1);
        Assert.True(engineE.NitrineTimeMultiplier > 1);
        Assert.True(engineE.NitrineTimeMultiplier < engineC.NitrineTimeMultiplier);

        double vacuumFuelC = engineC.CalculateFuelConsumption(distance);
        double vacuumFuelE = engineE.CalculateFuelConsumption(distance);
        double nitrineFuelC = engineC.CalculateFuelConsumption(distance, engineC.NitrineTimeMultiplier);
        double nitrineFuelE = engineE.CalculateFuelConsumption(distance, engineE.NitrineTimeMultiplier);

        Assert.True(nitrineFuelC > vacuumFuelC);
        Assert.True(nitrineFuelE > vacuumFuelE);
        Assert.True(nitrineFuelE < nitrineFuelC);
    }

    [Fact]
    public void JumpTravelTime_IsDistanceDividedByJumpSpeed()
    {
        var engine = new JumpEngineGamma();

        Assert.Equal(2, engine.CalculateTravelTime(20), Precision);
    }

    [Fact]
    public void JumpTravelTime_RejectsDistanceBeyondMaxDistance()
    {
        var engine = new JumpEngineAlpha();

        Assert.Throws<ArgumentOutOfRangeException>(() => engine.CalculateTravelTime(31));
    }
}
