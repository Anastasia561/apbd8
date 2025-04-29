using apbd8.Model;
using apbd8.Model.Dto;
using apbd8.Repositories;
using apbd8.Services.Mappers;

namespace apbd8.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<bool> ValidateClientAsync(int id, CancellationToken cancellationToken)
    {
        return await _clientRepository.CheckIfClientExistsAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<ClientTrip>> GetClientTripsAsync(int id, CancellationToken cancellationToken)
    {
        return await _clientRepository.GetClientTripAsync(id, cancellationToken);
    }

    public async Task<int> CreateClientAsync(ClientCreateRequestDto clientDto, CancellationToken cancellationToken)
    {
        var client = ClientMapper.MapClient(clientDto);

        if (!client.IsValidEmail() && !client.IsValidPhone() && !client.IsValidPesel())
        {
            throw new ArgumentException("Invalid client data provided");
        }

        return await _clientRepository.CreateClientAsync(client, cancellationToken);
    }

    public async Task RegisterClientForTripAsync(int clientId, int tripId, CancellationToken cancellationToken)
    {
        await _clientRepository.UpdateClientTripAsync(clientId, tripId, cancellationToken);
    }
}