using apbd8.Model;
using apbd8.Model.Dto;
using apbd8.Repositories;
using apbd8.Services.Mappers;

namespace apbd8.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<IEnumerable<TripDto>> GetTripsAsync(CancellationToken cancellationToken)
    {
        var trips = await _tripRepository.GetTripsAsync(cancellationToken);
        return trips.Select(trip => TripMapper.MapTrip(trip)).ToList();
    }
}