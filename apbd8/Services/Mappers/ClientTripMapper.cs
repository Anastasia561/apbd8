using apbd8.Model;
using apbd8.Model.Dto;

namespace apbd8.Services.Mappers;

public class ClientTripMapper
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
}