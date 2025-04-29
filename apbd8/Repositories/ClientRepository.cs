using System.Globalization;
using apbd8.Model;
using Microsoft.Data.SqlClient;

namespace apbd8.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly string _connectionString;

    public ClientRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<int> CreateClient(Client client, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        await con.OpenAsync(cancellationToken);

        var com = new SqlCommand("insert into client(FirstName, LastName, Email, Telephone, Pesel)" +
                                 "values (@firstName, @lastName, @email, @phone, @pesel); select cast(scope_identity() as int);",
            con);
        com.Parameters.AddWithValue("@firstName", client.FirstName);
        com.Parameters.AddWithValue("@lastName", client.LastName);
        com.Parameters.AddWithValue("@email", client.Email);
        com.Parameters.AddWithValue("@phone", client.Phone);
        com.Parameters.AddWithValue("@pesel", client.Pesel);

        var result = (int)await com.ExecuteScalarAsync(cancellationToken);

        await con.DisposeAsync();
        return result;
    }

    public async Task<IEnumerable<ClientTrip>> GetClientTripAsync(int id, CancellationToken cancellationToken)
    {
        if (!await CheckIfClientExistsAsync(id, cancellationToken))
        {
            throw new ArgumentException($"Client with id - {id} not found");
        }

        if (!await CheckIfClientHasTripsAsync(id, cancellationToken))
        {
            throw new ArgumentException($"Client with id - {id} has no trips");
        }

        return await GetClientWithTripsAsync(id, cancellationToken);
    }

    private async Task<IEnumerable<ClientTrip>> GetClientWithTripsAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        await con.OpenAsync(cancellationToken);
        var com = new SqlCommand("select * from Client_Trip where IdClient=@id", con);
        com.Parameters.AddWithValue("@id", id);
        var clientTrips = new List<ClientTrip>();
        await using (var reader = await com.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                var clientTrip = new ClientTrip
                {
                    TripId = (int)reader["IdTrip"],
                    RegisteredDate = DateOnly.ParseExact(reader["RegisteredAt"].ToString(), "yyyyMMdd",
                        CultureInfo.InvariantCulture),
                    PaymentDate = DateOnly.ParseExact(reader["PaymentDate"].ToString(), "yyyyMMdd",
                        CultureInfo.InvariantCulture)
                };
                clientTrips.Add(clientTrip);
            }
        }

        await con.DisposeAsync();
        return clientTrips;
    }

    private async Task<bool> CheckIfClientExistsAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        await con.OpenAsync(cancellationToken);

        var com = new SqlCommand("select count(*) from Client where IdClient=@id", con);
        com.Parameters.AddWithValue("@id", id);

        var result = (int)await com.ExecuteScalarAsync(cancellationToken);

        await con.DisposeAsync();
        return result > 0;
    }

    private async Task<bool> CheckIfClientHasTripsAsync(int id, CancellationToken cancellationToken)
    {
        var con = new SqlConnection(_connectionString);
        await con.OpenAsync(cancellationToken);

        var com = new SqlCommand("select count(*) from Client_Trip where IdClient=@id", con);
        com.Parameters.AddWithValue("@id", id);

        var result = (int)await com.ExecuteScalarAsync(cancellationToken);

        await con.DisposeAsync();
        return result > 0;
    }
}