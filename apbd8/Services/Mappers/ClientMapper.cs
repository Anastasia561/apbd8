using apbd8.Model;
using apbd8.Model.Dto;

namespace apbd8.Services.Mappers;

public class ClientMapper
{
    public static IEnumerable<ClientTripDto> MapClientTrips(IEnumerable<TripDto> trips,
        IEnumerable<ClientTrip> clientTrips)
    {
        return clientTrips.Select(clientTrip => new ClientTripDto()
        {
            Trip = trips.FirstOrDefault(t => t.Id == clientTrip.TripId),
            RegisteredDate = clientTrip.RegisteredDate, PaymentDate = clientTrip.PaymentDate
        }).ToList();
    }

    public static Client MapClient(ClientCreateRequestDto clientDto)
    {
        return new Client
        {
            FirstName = clientDto.FirstName,
            LastName = clientDto.LastName,
            Email = clientDto.Email,
            Phone = clientDto.Phone,
            Pesel = clientDto.Pesel
        };
    }
}