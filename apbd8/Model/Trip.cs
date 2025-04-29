namespace apbd8.Model;

public class Trip
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Destination { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<Country> Countries { get; set; }
}