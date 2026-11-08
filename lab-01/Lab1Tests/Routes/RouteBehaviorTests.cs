using Lab1.Environments;
using Lab1.Fuel;
using Lab1.Obstacles;
using Lab1.Routes;
using Lab1.Ships;

namespace Lab1Tests.Routes;

public class RouteBehaviorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void RouteSegment_RejectsNonPositiveDistance(double distance)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new RouteSegment(distance, new NormalSpace()));
    }

    [Fact]
    public void Route_RejectsEmptySegmentList()
    {
        Assert.Throws<ArgumentException>(() => new Route());
    }

    [Fact]
    public void ImpassableJump_IsShipLost_AndDoesNotApplyObstacles()
    {
        var nebula = new HighDensityNebula();
        nebula.AddObstacle(new AntimatterFlash());
        var ship = new Augur();

        RouteResult result = Simulator().Traverse(new Route(new RouteSegment(50, nebula)), ship);

        Assert.IsType<ShipLost>(result);
        Assert.True(ship.Defense.IsCrewAlive);
        Assert.True(ship.Defense.Deflector!.IsActive);
    }

    [Fact]
    public void Augur_SucceedsAtExactAlphaRange()
    {
        RouteResult result = Simulator().Traverse(new Route(new RouteSegment(30, new HighDensityNebula())), new Augur());

        Assert.IsType<Success>(result);
    }

    [Fact]
    public void DefenseDamage_PersistsWhenLaterSegmentIsLost()
    {
        var normal = new NormalSpace();
        normal.AddObstacle(new SmallAsteroid());
        var ship = new Stella();
        var route = new Route(
            new RouteSegment(10, normal),
            new RouteSegment(150, new HighDensityNebula()));

        RouteResult result = Simulator().Traverse(route, ship);

        Assert.IsType<ShipLost>(result);
        Assert.True(ship.Defense.Deflector!.IsActive);

        new SmallAsteroid().Strike(ship.Defense);
        Assert.False(ship.Defense.Deflector.IsActive);
        Assert.False(ship.Defense.IsShipDestroyed);
    }

    [Fact]
    public void Selector_ReturnsNoSuitableShip_WhenNobodyFinishes()
    {
        var route = new Route(new RouteSegment(50, new HighDensityNebula()));
        ShipSelection selection = new OptimalShipSelector(Simulator()).Select(
            route,
            new Ship[] { new PleasureShuttle(), new Meridian() });

        Assert.IsType<NoSuitableShip>(selection);
    }

    [Fact]
    public void OptimalShipSelector_DoesNotMutateCandidateShips()
    {
        var space = new NormalSpace();
        space.AddObstacle(new SmallAsteroid());
        var route = new Route(new RouteSegment(10, space));
        var shuttle = new PleasureShuttle();
        var vaklas = new Vaklas();

        ShipSelection selection = new OptimalShipSelector(Simulator()).Select(route, new Ship[] { shuttle, vaklas });

        Assert.IsType<OptimalShip>(selection);
        Assert.False(shuttle.Defense.IsShipDestroyed);
        new SmallAsteroid().Strike(shuttle.Defense);
        Assert.False(shuttle.Defense.IsShipDestroyed);

        Assert.True(vaklas.Defense.Deflector!.IsActive);
        new SmallAsteroid().Strike(vaklas.Defense);
        Assert.True(vaklas.Defense.Deflector.IsActive);
    }

    private static RouteSimulator Simulator()
    {
        return new RouteSimulator(new FuelExchange(2, 3));
    }
}
