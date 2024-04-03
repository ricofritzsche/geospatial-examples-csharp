namespace BoundingBox.GeoSpatial;

using System;
using Xunit;
using FluentAssertions;

public class BoundingBoxTests
{
    [Fact]
    public void CreateFromParameters_ShouldCreateValidBoundingBox_WhenWithin60Km()
    {
        // Coordinates roughly around a central point in Germany, within 60 km
        double left = 13.0, bottom = 52.0, right = 13.5, top = 52.2;

        var action = () => BoundingBox.CreateFromParameters(left, bottom, right, top);

        action.Should().NotThrow();
    }

    [Fact]
    public void CreateFromParameters_ShouldThrow_WhenExceeding60Km()
    {
        // Coordinates that would span more than 60 km
        double left = 13.0, bottom = 52.0, right = 14.0, top = 53.0;

        var action = () => BoundingBox.CreateFromParameters(left, bottom, right, top);

        action.Should().Throw<ArgumentException>()
            .WithMessage("*Bounding box size exceeds maximum allowed 60 km.*");
    }

    [Fact]
    public void CreateFromParameters_ShouldThrow_WhenInvalidCoordinates()
    {
        // Coordinates where bottomLeft is not below and to the left of topRight
        double left = 14.0, bottom = 53.0, right = 13.0, top = 52.0;

        var action = () => BoundingBox.CreateFromParameters(left, bottom, right, top);

        action.Should().Throw<ArgumentException>()
            .WithMessage("*Invalid bounding box coordinates.*");
    }
}
