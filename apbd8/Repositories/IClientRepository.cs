using apbd8.Model;

namespace apbd8.Repositories;

public interface IClientRepository
{
    public Task<IEnumerable<ClientTrip>> GetClientTripAsync(int id, CancellationToken cancellationToken);
    public Task<int> CreateClientAsync(Client client, CancellationToken cancellationToken);
    public Task UpdateClientTripAsync(int clientId, int tripId, CancellationToken cancellationToken);
    public Task<bool> CheckIfClientExistsAsync(int id, CancellationToken cancellationToken);
}