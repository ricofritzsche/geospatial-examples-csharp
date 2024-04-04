using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoundingBox.Features.GetMapTilesByBbox;

[ApiController]
[Route("api/v1/maps")]
public class GetMapTilesByBboxEndpoint(IMediator mediator) : ControllerBase
{
    [HttpGet("{bbox}", Name = "GetMapTilesByBbox")]
    public async Task<ActionResult<MapTileResponse>> Get(string bbox)
    {
        double left, bottom, right, top;

        if (!TryParseBbox(bbox, out left, out bottom, out right, out top))
        {
            return BadRequest("The bounding box must contain four comma-separated numeric values.");
        }

        try
        {
            var boundingBox = GeoSpatial.BoundingBox.CreateFromParameters(left, bottom, right, top);
            var result = await mediator.Send(new GetMapTilesByBboxQuery(boundingBox));
            return Ok(result);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch
        {
            return StatusCode(500, "An internal server error occurred.");
        }
    }

    private bool TryParseBbox(string bbox, out double left, out double bottom, out double right, out double top)
    {
        var parts = bbox.Split(',');

        if (parts.Length != 4 ||
            !double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out left) ||
            !double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out bottom) ||
            !double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out right) ||
            !double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out top))
        {
            left = bottom = right = top = 0;
            return false;
        }

        return true;
    }
}

public record MapTileResponse (string answer);
