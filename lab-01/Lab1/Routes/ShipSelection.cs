using Lab1.Ships;

namespace Lab1.Routes;

public abstract record ShipSelection;

public sealed record OptimalShip(Ship Ship, Success Flight) : ShipSelection;

public sealed record NoSuitableShip : ShipSelection;
