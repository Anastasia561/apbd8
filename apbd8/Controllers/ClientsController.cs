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

//Endpoint for getting a list of all trips along with destination coutries
//for a specific client (trips they are registered for)
    [HttpGet("{id}/trips")]
    public async Task<IActionResult> GetClientTripsAsync(int id)
    {
        var trips = await _tripService.GetTripsAsync(CancellationToken.None);
        var clientTrips = await _clientService.GetClientTripsAsync(id, CancellationToken.None);
        var result = ClientMapper.MapClientTrips(trips, clientTrips);
        return Ok(result);
    }

//Endpoint for creating new client record
//with parameters - firstName, lastName, email, phone number, PESEL number
//input parameters (email, phone number, PESEL number) are validated for correct format
    [HttpPost]
    public async Task<IActionResult> CreateClientAsync(ClientCreateRequestDto clientDto,
        CancellationToken cancellationToken)
    {
        var result = await _clientService.CreateClientAsync(clientDto, cancellationToken);
        return Ok(result);
    }

//endpoint for registering existing client for an existing trip
//input ids of client and trip are validated to be present in DB
    [HttpPut("{id}/trips/{tripId}")]
    public async Task<IActionResult> RegisterForTripAsync(int id, int tripId, CancellationToken cancellationToken)
    {
        if (!await _tripService.ValidateTripAsync(tripId, cancellationToken) ||
            !await _clientService.ValidateClientAsync(id, cancellationToken)) return BadRequest();

        await _clientService.RegisterClientForTripAsync(id, tripId, cancellationToken);
        return Ok();
    }

//Endpoint for deleting registration of an existing client for an existing trip
//input ids of client and trip are validated to be present in DB
    [HttpDelete("{id}/trips/{tripId}")]
    public async Task<IActionResult> UnregisterForTripAsync(int id, int tripId, CancellationToken cancellationToken)
    {
        await _clientService.DeleteClientRegistrationAsync(id, tripId, cancellationToken);
        return Ok();
    }
}