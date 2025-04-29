using apbd8.Model;

namespace apbd8.Repositories;

public interface IClientRepository
{
    public Task<IEnumerable<ClientTrip>> GetClientTripAsync(int id, CancellationToken cancellationToken);
    public Task<int> CreateClient(Client client, CancellationToken cancellationToken);
}