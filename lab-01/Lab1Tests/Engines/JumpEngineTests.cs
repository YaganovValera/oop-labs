using Lab1.Engines;

namespace Lab1Tests.Engines;

public class JumpEngineTests
{
    private const int Precision = 8;

    [Fact]
    public void Alpha_MaxDistance_Is30()
    {
        var engine = new JumpEngineAlpha();

        Assert.Equal(30, engine.MaxDistance, Precision);
    }

    [Fact]
    public void Omega_MaxDistance_Is100()
    {
        var engine = new JumpEngineOmega();

        Assert.Equal(100, engine.MaxDistance, Precision);
    }

    [Fact]
    public void Gamma_MaxDistance_Is500()
    {
        var engine = new JumpEngineGamma();

        Assert.Equal(500, engine.MaxDistance, Precision);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(30)]
    public void CanJump_ReturnsTrue_ForAllowedDistance(double distance)
    {
        IJumpEngine engine = new JumpEngineAlpha();

        Assert.True(engine.CanJump(distance));
    }

    [Fact]
    public void CanJump_ReturnsFalse_WhenDistanceExceedsMaxDistance()
    {
        IJumpEngine alpha = new JumpEngineAlpha();
        IJumpEngine omega = new JumpEngineOmega();
        IJumpEngine gamma = new JumpEngineGamma();

        Assert.False(alpha.CanJump(31));
        Assert.False(omega.CanJump(101));
        Assert.False(gamma.CanJump(501));
    }

    [Fact]
    public void Alpha_FuelConsumption_IsLinear()
    {
        var engine = new JumpEngineAlpha();

        Assert.Equal(20, engine.CalculateFuelConsumption(10), Precision);
    }

    [Fact]
    public void Omega_FuelConsumption_IsDistanceTimesLog2()
    {
        var engine = new JumpEngineOmega();
        const double distance = 10;

        double fuel = engine.CalculateFuelConsumption(distance);

        Assert.Equal(distance * Math.Log2(distance + 1), fuel, Precision);
    }

    [Fact]
    public void Gamma_FuelConsumption_IsQuadratic()
    {
        var engine = new JumpEngineGamma();

        Assert.Equal(50, engine.CalculateFuelConsumption(10), Precision);
    }

    [Fact]
    public void CalculateFuelConsumption_RejectsDistanceBeyondMaxDistance()
    {
        IJumpEngine alpha = new JumpEngineAlpha();
        IJumpEngine omega = new JumpEngineOmega();
        IJumpEngine gamma = new JumpEngineGamma();

        Assert.Throws<ArgumentOutOfRangeException>(() => alpha.CalculateFuelConsumption(31));
        Assert.Throws<ArgumentOutOfRangeException>(() => omega.CalculateFuelConsumption(101));
        Assert.Throws<ArgumentOutOfRangeException>(() => gamma.CalculateFuelConsumption(501));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void CanJump_RejectsNonPositiveDistance(double distance)
    {
        IJumpEngine engine = new JumpEngineAlpha();

        Assert.Throws<ArgumentOutOfRangeException>(() => engine.CanJump(distance));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void CalculateFuelConsumption_RejectsNonPositiveDistance(double distance)
    {
        IJumpEngine alpha = new JumpEngineAlpha();
        IJumpEngine omega = new JumpEngineOmega();
        IJumpEngine gamma = new JumpEngineGamma();

        Assert.Throws<ArgumentOutOfRangeException>(() => alpha.CalculateFuelConsumption(distance));
        Assert.Throws<ArgumentOutOfRangeException>(() => omega.CalculateFuelConsumption(distance));
        Assert.Throws<ArgumentOutOfRangeException>(() => gamma.CalculateFuelConsumption(distance));
    }
}
