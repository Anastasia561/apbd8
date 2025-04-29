using apbd8.Model;
using Microsoft.Data.SqlClient;

namespace apbd8.Repositories;

public class TripRepository : ITripRepository
{
    private readonly string _connectionString;

    public TripRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> CheckIfTripExistsAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        await con.OpenAsync(cancellationToken);

        var com = new SqlCommand("select count(*) from Trip where IdTrip=@id", con);
        com.Parameters.AddWithValue("@id", id);

        var result = (int)await com.ExecuteScalarAsync(cancellationToken);

        await con.DisposeAsync();
        return result > 0;
    }

    public async Task<bool> CheckIfTripHasMaxPeopleAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        await con.OpenAsync(cancellationToken);

        var com = new SqlCommand("select count(*) from Client_Trip where IdTrip=@id", con);
        com.Parameters.AddWithValue("@id", id);

        var peopleRegistered = (int)await com.ExecuteScalarAsync(cancellationToken);
        var maxPeople = await GetMaxNumberOfPeopleForTripAsync(id, cancellationToken);
        await con.DisposeAsync();
        return peopleRegistered >= maxPeople;
    }

    private async Task<int> GetMaxNumberOfPeopleForTripAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        await con.OpenAsync(cancellationToken);

        var com = new SqlCommand("select MaxPeople from Trip where IdTrip=@id", con);
        com.Parameters.AddWithValue("@id", id);

        var result = (int)await com.ExecuteScalarAsync(cancellationToken);
        await con.DisposeAsync();
        return result;
    }

    public async Task<IEnumerable<Trip>> GetTripsAsync(CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        await con.OpenAsync(cancellationToken);

        var com1 = new SqlCommand("select * from trip", con);
        var trips = new List<Trip>();
        await using (var reader1 = await com1.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader1.ReadAsync(cancellationToken))
            {
                var trip = new Trip
                {
                    Id = (int)reader1["IdTrip"],
                    Name = reader1["Name"].ToString(),
                    Description = reader1["Description"].ToString(),
                    DateFrom = DateTime.Parse(reader1["DateFrom"].ToString()),
                    DateTo = DateTime.Parse(reader1["DateTo"].ToString()),
                    MaxPeople = (int)reader1["MaxPeople"],
                    Countries = []
                };
                trips.Add(trip);
            }
        }

        var com2 = new SqlCommand(
            "select ct.IdTrip, c.* from Country_Trip ct join Country c on ct.IdCountry = c.IdCountry", con);
        await using (var reader2 = await com2.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader2.ReadAsync(cancellationToken))
            {
                var trip = trips.Find(t => t.Id == (int)reader2["IdTrip"]);
                trip?.Countries.Add(new Country
                {
                    Id = (int)reader2["IdCountry"],
                    Name = reader2["Name"].ToString()
                });
            }
        }

        await con.DisposeAsync();
        return trips;
    }
}