using GoogleMapInfo;
using Microsoft.AspNetCore.Mvc;

namespace microservice_map_distance.Controllers
{
    [Route("api/mapinfo")]
    [ApiController]
    public class MapInfoController : ControllerBase
    {
        private readonly GoogleDistanceApi _googleDistanceApi;

        public MapInfoController(GoogleDistanceApi googleDistanceApi)
        {
            _googleDistanceApi = googleDistanceApi;
        }

        [HttpGet]
        public async Task<ActionResult<GoogleDistanceData>> GetDistance(
            string originCity,
            string destinationCity) =>
                await _googleDistanceApi.GetMapDistance(originCity, destinationCity);

    }
}
