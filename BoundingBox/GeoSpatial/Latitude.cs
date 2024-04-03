namespace BoundingBox.GeoSpatial;

public readonly record struct Latitude
{
    private const double MinLatitude = 47.3;
    private const double MaxLatitude = 55.1;
    
    public double Value { get; init; }

    public Latitude(double value)
    {
        if (value is < MinLatitude or > MaxLatitude)
            throw new ArgumentOutOfRangeException(nameof(value), $"Latitude must be between {MinLatitude} and {MaxLatitude} degrees for Germany.");
        
        Value = value;
    }
}
