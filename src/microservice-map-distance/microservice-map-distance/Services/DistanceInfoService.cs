using GoogleMapInfo;
using Grpc.Core;
using microservice_map_distance.Protos;

namespace microservice_map_distance.Services;

public class DistanceInfoService : DistanceInfo.DistanceInfoBase
{
    private readonly ILogger<DistanceInfoService> _logger;
    private readonly GoogleDistanceApi _googleDistanceApi;

    public DistanceInfoService(
        ILogger<DistanceInfoService> logger,
        GoogleDistanceApi googleDistanceApi)
    {
        _logger = logger;
        _googleDistanceApi = googleDistanceApi;
    }

    public override async Task<DistanceData> GetDistance(Cities cities, ServerCallContext context)
    {
        var totalKms = "0";

        var distanceData = await _googleDistanceApi.GetMapDistance(cities.OriginCity, cities.DestinationCity);
        foreach (var distanceDataRow in distanceData.rows)
        {
            foreach (var element in distanceDataRow.elements)
            {
                totalKms += element.distance.text;
            }
        }

        return new DistanceData { Kms = totalKms };
    }
}
