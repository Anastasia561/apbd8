using apbd8.Model;

namespace apbd8.Repositories;

public interface ITripRepository
{
    public Task<IEnumerable<Trip>> GetTripsAsync(CancellationToken cancellationToken);
}