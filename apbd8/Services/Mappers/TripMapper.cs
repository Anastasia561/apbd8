using apbd8.Model;
using apbd8.Model.Dto;

namespace apbd8.Services.Mappers;

public class TripMapper
{
    public static TripDto MapTrip(Trip trip)
    {
        return new TripDto()
        {
            Id = trip.Id,
            Name = trip.Name,
            Description = trip.Description,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            MaxPeople = trip.MaxPeople,
            Countries = trip.Countries,
        };
    }
}