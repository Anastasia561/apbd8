using apbd8.Model;
using apbd8.Model.Dto;
using apbd8.Repositories;

namespace apbd8.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<ClientTrip>> GetClientTripsAsync(int id, CancellationToken cancellationToken)
    {
        return await _clientRepository.GetClientTripAsync(id, cancellationToken);
    }
}