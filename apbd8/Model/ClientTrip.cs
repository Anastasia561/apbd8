namespace apbd8.Model;

public class ClientTrip
{
    public int TripId { get; set; }
    public DateOnly RegisteredDate { get; set; }
    public DateOnly PaymentDate { get; set; }
}