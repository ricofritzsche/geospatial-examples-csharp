namespace BoundingBox.GeoSpatial;

public readonly record struct Longitude
{
    private const double MinLongitude = 5.9;
    private const double MaxLongitude = 15.0;
    
    public double Value { get; init; }

    public Longitude(double value)
    {
        if (value is < MinLongitude or > MaxLongitude)
            throw new ArgumentOutOfRangeException(nameof(value), $"Longitude must be between {MinLongitude} and {MaxLongitude} degrees for Germany.");
        
        Value = value;
    }
}