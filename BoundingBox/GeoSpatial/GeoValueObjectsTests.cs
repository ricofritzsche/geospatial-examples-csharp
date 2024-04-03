namespace BoundingBox.GeoSpatial;

using System;
using Xunit;
using FluentAssertions;

public class GeoValueObjectsTests
{
    [Theory]
    [InlineData(47.3)] // Minimum valid latitude for Germany
    [InlineData(55.1)] // Maximum valid latitude for Germany
    [InlineData(51.0)] // An arbitrary valid latitude within Germany
    public void Latitude_ShouldAllowValidValuesWithinGermany(double value)
    {
        var action = () => new Latitude(value);

        action.Should().NotThrow();
    }

    [Theory]
    [InlineData(47.2)] // Just below the minimum latitude
    [InlineData(55.2)] // Just above the maximum latitude
    public void Latitude_ShouldThrowForValuesOutsideGermany(double value)
    {
        var action = () => new Latitude(value);

        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("*Latitude must be between 47.3 and 55.1 degrees for Germany.*");
    }

    [Theory]
    [InlineData(5.9)] // Minimum valid longitude for Germany
    [InlineData(15.0)] // Maximum valid longitude for Germany
    [InlineData(10.0)] // An arbitrary valid longitude within Germany
    public void Longitude_ShouldAllowValidValuesWithinGermany(double value)
    {
        var action = () => new Longitude(value);

        action.Should().NotThrow();
    }


    [Theory]
    [InlineData(5.8)] // Just below the minimum longitude
    [InlineData(15.1)] // Just above the maximum longitude
    public void Longitude_ShouldThrowForValuesOutsideGermany(double value)
    {
        var action = () => new Longitude(value);

        action.Should().Throw<ArgumentOutOfRangeException>()
            .And.ParamName.Should().Be("value", because: "the longitude value is outside the valid range for Germany");
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("*Longitude must be between*degrees for Germany.*", because: "values outside 5.9 and 15.0 are invalid");
    }
}
