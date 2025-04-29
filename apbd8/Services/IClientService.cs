using apbd8.Model;
using apbd8.Model.Dto;

namespace apbd8.Services;

public interface IClientService
{
    public Task<IEnumerable<ClientTrip>> GetClientTripsAsync(int id, CancellationToken cancellationToken);

    public Task<int> CreateClientAsync(ClientCreateRequestDto clientDto, CancellationToken cancellationToken);
    public Task<bool> ValidateClientAsync(int id, CancellationToken cancellationToken);
    public Task RegisterClientForTripAsync(int clientId, int tripId, CancellationToken cancellationToken);
    public Task DeleteClientRegistrationAsync(int clientId, int tripId, CancellationToken cancellationToken);
}