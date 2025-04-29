using apbd8.Model.Dto;
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
        var result = ClientMapper.MapClientTrips(trips, clientTrips);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClientAsync(ClientCreateRequestDto clientDto,
        CancellationToken cancellationToken)
    {
        var result = await _clientService.CreateClientAsync(clientDto, cancellationToken);
        return Ok(result);
    }
}