using Lab1.Fuel;

namespace Lab1.Routes;

public abstract record RouteResult;

public sealed record Success(double Time, FuelConsumption Fuel, double FuelCost) : RouteResult;

public sealed record ShipLost : RouteResult;

public sealed record ShipDestroyed : RouteResult;

public sealed record CrewDead : RouteResult;
