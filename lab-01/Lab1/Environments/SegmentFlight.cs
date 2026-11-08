using Lab1.Fuel;

namespace Lab1.Environments;

public abstract record SegmentFlight;

public sealed record SegmentCompleted(double Time, FuelConsumption Fuel) : SegmentFlight;

public sealed record SegmentDenied : SegmentFlight;
