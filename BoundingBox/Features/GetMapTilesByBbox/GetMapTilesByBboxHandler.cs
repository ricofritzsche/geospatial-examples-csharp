using MediatR;

namespace BoundingBox.Features.GetMapTilesByBbox;

public record GetMapTilesByBboxQuery(GeoSpatial.BoundingBox bbox) : IRequest<MapTileResponse>;

public class GetMapTilesByBboxQueryHandler : IRequestHandler<GetMapTilesByBboxQuery, MapTileResponse>
{
    public async Task<MapTileResponse> Handle(GetMapTilesByBboxQuery request, CancellationToken cancellationToken)
    {
        return new MapTileResponse("Here are your map tiles.");
    }
}