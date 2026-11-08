using Lab1.Environments;
using Lab1.Fuel;
using Lab1.Ships;

namespace Lab1.Routes;

public sealed class RouteSimulator
{
    private readonly FuelExchange _exchange;

    public RouteSimulator(FuelExchange exchange)
    {
        ArgumentNullException.ThrowIfNull(exchange);
        _exchange = exchange;
    }

    public RouteResult Traverse(Route route, Ship ship)
    {
        ArgumentNullException.ThrowIfNull(route);
        ArgumentNullException.ThrowIfNull(ship);

        double time = 0;
        FuelConsumption fuel = FuelConsumption.None;

        foreach (RouteSegment segment in route.Segments)
        {
            if (ship.Defense.IsShipDestroyed)
            {
                return new ShipDestroyed();
            }

            if (!ship.Defense.IsCrewAlive)
            {
                return new CrewDead();
            }

            SegmentFlight flight = segment.Environment.Fly(ship, segment.Distance);
            if (flight is not SegmentCompleted completed)
            {
                return new ShipLost();
            }

            foreach (var obstacle in segment.Environment.Obstacles)
            {
                obstacle.Strike(ship.Defense);

                if (ship.Defense.IsShipDestroyed)
                {
                    return new ShipDestroyed();
                }

                if (!ship.Defense.IsCrewAlive)
                {
                    return new CrewDead();
                }
            }

            time += completed.Time;
            fuel += completed.Fuel;
        }

        return new Success(time, fuel, _exchange.CalculateCost(fuel));
    }
}
