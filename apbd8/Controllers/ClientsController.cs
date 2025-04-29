using apbd8.Services;
using apbd8.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace apbd8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ITripService _tripService;
    private readonly IClientService _clientService;

    public ClientsController(ITripService tripService, IClientService clientService)
    {
        _tripService = tripService;
        _clientService = clientService;
    }

    [HttpGet("{id}/trips")]
    public async Task<IActionResult> GetClientTripsAsync(int id)
    {
        var trips = await _tripService.GetTripsAsync(CancellationToken.None);
        var clientTrips = await _clientService.GetClientTripsAsync(id, CancellationToken.None);
        var result = ClientTripMapper.MapClientTrips(trips, clientTrips);
        return Ok(result);
    }
}