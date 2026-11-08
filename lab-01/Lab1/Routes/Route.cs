namespace Lab1.Routes;

public sealed class Route
{
    public Route(params RouteSegment[] segments)
    {
        ArgumentNullException.ThrowIfNull(segments);

        if (segments.Length == 0)
        {
            throw new ArgumentException("Route must contain at least one segment.", nameof(segments));
        }

        Segments = Array.AsReadOnly(segments.ToArray());
    }

    public IReadOnlyList<RouteSegment> Segments { get; }
}
