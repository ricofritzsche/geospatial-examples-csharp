namespace BoundingBox.GeoSpatial;

public readonly record struct BoundingBox
{
    public GeoPosition BottomLeft { get; init; }
    public GeoPosition TopRight { get; init; }

    private BoundingBox(GeoPosition bottomLeft, GeoPosition topRight)
    {
        BottomLeft = bottomLeft;
        TopRight = topRight;
    }
    
    private static double CalculateDistanceInKilometers(GeoPosition pos1, GeoPosition pos2)
    {
        // the Haversine formula calculates the great-circle distance between two points.
        
        var earthRadiusKm = 6371.0;

        var dLat = ToRadians(pos2.Latitude.Value - pos1.Latitude.Value);
        var dLon = ToRadians(pos2.Longitude.Value - pos1.Longitude.Value);

        var lat1 = ToRadians(pos1.Latitude.Value);
        var lat2 = ToRadians(pos2.Latitude.Value);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadiusKm * c;
    }

    private static double ToRadians(double val) => (Math.PI / 180) * val;

    public static BoundingBox CreateFromParameters(double left, double bottom, double right, double top)
    {
        var minLongitude = new Longitude(left);
        var minLatitude = new Latitude(bottom);
        var maxLongitude = new Longitude(right);
        var maxLatitude = new Latitude(top);

        var bottomLeft = new GeoPosition(minLatitude, minLongitude);
        var topRight = new GeoPosition(maxLatitude, maxLongitude);

        if (bottomLeft.Latitude.Value >= topRight.Latitude.Value ||
            bottomLeft.Longitude.Value >= topRight.Longitude.Value)
        {
            throw new ArgumentException("Invalid bounding box coordinates.");
        }

        var distanceAcrossDiagonal = CalculateDistanceInKilometers(bottomLeft, topRight);
        if (distanceAcrossDiagonal > 60)
        {
            throw new ArgumentException("Bounding box size exceeds maximum allowed 60 km.");
        }

        return new BoundingBox(bottomLeft, topRight);
    }

}