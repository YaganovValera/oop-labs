using Lab1.Ships;

namespace Lab1.Routes;

public sealed class OptimalShipSelector
{
    private const double FuelCostTolerance = 1e-6;

    private readonly RouteSimulator _simulator;

    public OptimalShipSelector(RouteSimulator simulator)
    {
        ArgumentNullException.ThrowIfNull(simulator);
        _simulator = simulator;
    }

    public ShipSelection Select(Route route, IReadOnlyList<Ship> candidates)
    {
        ArgumentNullException.ThrowIfNull(route);
        ArgumentNullException.ThrowIfNull(candidates);

        Ship? bestShip = null;
        Success? bestFlight = null;

        foreach (Ship candidate in candidates)
        {
            Ship simulation = candidate.CreateSimulationCopy();
            if (_simulator.Traverse(route, simulation) is not Success flight)
            {
                continue;
            }

            if (bestFlight is null || IsBetter(flight, bestFlight))
            {
                bestShip = candidate;
                bestFlight = flight;
            }
        }

        if (bestShip is null || bestFlight is null)
        {
            return new NoSuitableShip();
        }

        return new OptimalShip(bestShip, bestFlight);
    }

    private static bool IsBetter(Success candidate, Success current)
    {
        double costDelta = candidate.FuelCost - current.FuelCost;
        if (costDelta < -FuelCostTolerance)
        {
            return true;
        }

        return Math.Abs(costDelta) <= FuelCostTolerance && candidate.Time < current.Time;
    }
}
