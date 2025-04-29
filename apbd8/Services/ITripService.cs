using apbd8.Model;
using apbd8.Model.Dto;

namespace apbd8.Services;

public interface ITripService
{
    public Task<IEnumerable<TripDto>> GetTripsAsync(CancellationToken cancellationToken);
}