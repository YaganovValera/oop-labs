using Lab1.Environments;
using Lab1.Fuel;
using Lab1.Obstacles;
using Lab1.Routes;
using Lab1.Ships;

namespace Lab1Tests.Routes;

public class LaboratoryScenariosTests
{
    private const int Precision = 8;

    public static IEnumerable<object[]> ShipsThatCannotCrossMediumChannel()
    {
        yield return new object[] { new PleasureShuttle() };
        yield return new object[] { new Augur() };
    }

    public static IEnumerable<object[]> AntimatterFlashCases()
    {
        yield return new object[] { new Vaklas(), typeof(CrewDead) };
        yield return new object[] { Vaklas.WithPhotonDeflector(), typeof(Success) };
    }

    [Theory]
    [MemberData(nameof(ShipsThatCannotCrossMediumChannel))]
    public void MediumHighDensityNebula_PleasureShuttleAndAugur_DoNotSucceed(Ship ship)
    {
        var route = new Route(new RouteSegment(50, new HighDensityNebula()));

        RouteResult result = Simulator().Traverse(route, ship);

        Assert.IsType<ShipLost>(result);
    }

    [Theory]
    [MemberData(nameof(AntimatterFlashCases))]
    public void HighDensityNebulaWithAntimatterFlash_DependsOnPhotonDeflector(Ship ship, Type expectedResult)
    {
        var nebula = new HighDensityNebula();
        nebula.AddObstacle(new AntimatterFlash());
        var route = new Route(new RouteSegment(20, nebula));

        RouteResult result = Simulator().Traverse(route, ship);

        Assert.IsType(expectedResult, result);
    }

    public static IEnumerable<object[]> OneSpaceWhaleCases()
    {
        yield return new object[] { new SpaceWhaleCase(new Vaklas(), typeof(ShipDestroyed), true, null) };
        yield return new object[] { new SpaceWhaleCase(new Augur(), typeof(Success), false, false) };
        yield return new object[] { new SpaceWhaleCase(new Meridian(), typeof(Success), false, true) };
    }

    [Theory]
    [MemberData(nameof(OneSpaceWhaleCases))]
    public void NitrineNebulaWithOneSpaceWhale(SpaceWhaleCase whaleCase)
    {
        RouteResult result = FlyWhale(whaleCase.Ship);

        Assert.IsType(whaleCase.ExpectedResult, result);
        Assert.Equal(whaleCase.ShipDestroyed, whaleCase.Ship.Defense.IsShipDestroyed);
        if (whaleCase.DeflectorActive is bool active)
        {
            Assert.Equal(active, whaleCase.Ship.Defense.Deflector!.IsActive);
        }
    }

    [Fact]
    public void ShortNormalSpace_OptimalShipIsPleasureShuttle()
    {
        var route = new Route(new RouteSegment(10, new NormalSpace()));
        RouteSimulator simulator = Simulator();
        var shuttle = new PleasureShuttle();
        var vaklas = new Vaklas();

        Assert.IsType<Success>(simulator.Traverse(route, new PleasureShuttle()));
        Assert.IsType<Success>(simulator.Traverse(route, new Vaklas()));

        ShipSelection selection = new OptimalShipSelector(simulator).Select(route, new Ship[] { shuttle, vaklas });

        OptimalShip optimal = Assert.IsType<OptimalShip>(selection);
        Assert.Same(shuttle, optimal.Ship);
    }

    [Fact]
    public void MediumHighDensityNebula_OptimalShipIsStella()
    {
        var route = new Route(new RouteSegment(50, new HighDensityNebula()));
        RouteSimulator simulator = Simulator();
        var augur = new Augur();
        var stella = new Stella();

        Assert.IsType<ShipLost>(simulator.Traverse(route, new Augur()));
        Assert.IsType<Success>(simulator.Traverse(route, new Stella()));

        ShipSelection selection = new OptimalShipSelector(simulator).Select(route, new Ship[] { augur, stella });

        OptimalShip optimal = Assert.IsType<OptimalShip>(selection);
        Assert.Same(stella, optimal.Ship);
    }

    [Fact]
    public void NitrineNebula_OptimalShipIsVaklas()
    {
        var route = new Route(new RouteSegment(100, new NitrineNebula()));
        var exchange = new FuelExchange(3, 9);
        var simulator = new RouteSimulator(exchange);
        Success shuttleFlight = Assert.IsType<Success>(simulator.Traverse(route, new PleasureShuttle()));
        Success vaklasFlight = Assert.IsType<Success>(simulator.Traverse(route, new Vaklas()));
        Assert.True(vaklasFlight.FuelCost < shuttleFlight.FuelCost);

        var shuttle = new PleasureShuttle();
        var vaklas = new Vaklas();
        ShipSelection selection = new OptimalShipSelector(simulator).Select(route, new Ship[] { shuttle, vaklas });

        OptimalShip optimal = Assert.IsType<OptimalShip>(selection);
        Assert.Same(vaklas, optimal.Ship);
    }

    [Fact]
    public void MultiSegmentRoute_AccumulatesFlightAndKeepsDefenseState()
    {
        const double normalDistance = 10;
        const double nitrineDistance = 30;
        const double channelDistance = 40;
        var ship = Vaklas.WithPhotonDeflector();
        var exchange = new FuelExchange(2, 5);

        var normalSpace = new NormalSpace();
        normalSpace.AddObstacle(new SmallAsteroid());
        var nitrineNebula = new NitrineNebula();
        var channel = new HighDensityNebula();
        channel.AddObstacle(new AntimatterFlash());

        double expectedTime =
            ship.ImpulseEngine.CalculateTravelTime(normalDistance)
            + ship.ImpulseEngine.CalculateTravelTime(nitrineDistance, ship.ImpulseEngine.NitrineTimeMultiplier)
            + ship.JumpEngine!.CalculateTravelTime(channelDistance);
        double expectedPlasma =
            ship.ImpulseEngine.CalculateFuelConsumption(normalDistance)
            + ship.ImpulseEngine.CalculateFuelConsumption(nitrineDistance, ship.ImpulseEngine.NitrineTimeMultiplier);
        double expectedGraviton = ship.JumpEngine.CalculateFuelConsumption(channelDistance);
        var expectedFuel = new FuelConsumption(expectedPlasma, expectedGraviton);

        var route = new Route(
            new RouteSegment(normalDistance, normalSpace),
            new RouteSegment(nitrineDistance, nitrineNebula),
            new RouteSegment(channelDistance, channel));

        Success result = Assert.IsType<Success>(new RouteSimulator(exchange).Traverse(route, ship));

        Assert.Equal(expectedTime, result.Time, Precision);
        Assert.Equal(expectedFuel.ActivePlasma, result.Fuel.ActivePlasma, Precision);
        Assert.Equal(expectedFuel.GravitonMatter, result.Fuel.GravitonMatter, Precision);
        Assert.Equal(exchange.CalculateCost(expectedFuel), result.FuelCost, Precision);
        Assert.True(ship.Defense.IsCrewAlive);
        Assert.False(ship.Defense.IsShipDestroyed);
        Assert.True(ship.Defense.Deflector!.IsActive);
        Assert.Empty(nitrineNebula.Obstacles);

        new SmallAsteroid().Strike(ship.Defense);
        Assert.False(ship.Defense.Deflector.IsActive);
        Assert.False(ship.Defense.IsShipDestroyed);
    }

    private static RouteResult FlyWhale(Ship ship)
    {
        var nebula = new NitrineNebula();
        nebula.AddObstacle(new SpaceWhale(1));
        return Simulator().Traverse(new Route(new RouteSegment(25, nebula)), ship);
    }

    private static RouteSimulator Simulator()
    {
        return new RouteSimulator(new FuelExchange(2, 3));
    }

    public sealed record SpaceWhaleCase(Ship Ship, Type ExpectedResult, bool ShipDestroyed, bool? DeflectorActive);
}
