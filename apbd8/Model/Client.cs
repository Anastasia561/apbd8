using System.Text.RegularExpressions;

namespace apbd8.Model;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Pesel { get; set; }

    public bool IsValidEmail()
    {
        var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return emailRegex.IsMatch(Email);
    }

    public bool IsValidPhone()
    {
        var phoneRegex = new Regex(@"^\d{1,14}$");
        return phoneRegex.IsMatch(Phone);
    }

    public bool IsValidPesel()
    {
        var peselRegex = new Regex(@"^\d{11}$");
        return peselRegex.IsMatch(Pesel);
    }
}