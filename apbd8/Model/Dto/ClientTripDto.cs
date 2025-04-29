namespace apbd8.Model.Dto;

public class ClientTripDto
{
    public TripDto Trip { get; set; }
    public DateOnly RegisteredDate { get; set; }
    public DateOnly PaymentDate { get; set; }
}