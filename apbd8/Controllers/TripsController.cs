﻿using apbd8.Model.Dto;
using apbd8.Repositories;
using apbd8.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

//Endpoint for getting a list of all available trips in the system
//along with destination countries
    [HttpGet]
    public async Task<IActionResult> GetTripsAsync()
    {
        var trips = await _tripService.GetTripsAsync(CancellationToken.None);
        return Ok(trips);
    }
}