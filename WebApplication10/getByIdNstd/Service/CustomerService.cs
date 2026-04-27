using WebApplication10.Exceptions;
using WebApplication10.getByIdNstd.DTOs;

namespace WebApplication10.getByIdNstd.Service;

using Microsoft.Data.SqlClient;


public class CustomerService(IConfiguration configuration) : ICustomerService
{
    public async Task<CustomerDto> GetCustomerRentalsAsync(int customerId)
    {
        CustomerDto? dto = null;
        Dictionary<int, RentalDto> rentals = [];

        await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        await connection.OpenAsync();

        command.Connection = connection;
        command.CommandText = """
                              SELECT c.first_name,
                                     c.last_name,
                                     r.rental_id,
                                     r.rental_date,
                                     r.return_date,
                                     s.name,
                                     m.title,
                                     ri.price_at_rental
                              FROM Customer c
                              LEFT JOIN Rental r ON c.customer_id = r.customer_id
                              LEFT JOIN Status s ON s.status_id = r.status_id
                              LEFT JOIN Rental_Item ri ON ri.rental_id = r.rental_id
                              LEFT JOIN Movie m ON m.movie_id = ri.movie_id
                              WHERE c.customer_id = @customerId
                              """;

        command.Parameters.AddWithValue("@customerId", customerId);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            dto ??= new CustomerDto
            {
                FirstName = reader.GetString(0),
                LastName = reader.GetString(1),
                Rentals = []
            };

            if (reader.IsDBNull(2))
            {
                continue;
            }

            var rentalId = reader.GetInt32(2);

            if (!rentals.ContainsKey(rentalId))
            {
                rentals.Add(rentalId, new RentalDto
                {
                    Id = rentalId,
                    RentalDate = reader.GetDateTime(3),
                    ReturnDate = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                    Status = reader.GetString(5),
                    Movies = []
                });
            }

            var movies = rentals[rentalId].Movies.ToList();

            movies.Add(new MovieDto
            {
                Title = reader.GetString(6),
                PriceAtRental = reader.GetDecimal(7)
            });

            rentals[rentalId].Movies = movies;
        }

        if (dto is null)
        {
            throw new NotFoundException($"Customer with id {customerId} not found");
        }

        dto.Rentals = rentals.Values;

        return dto;
    }
}