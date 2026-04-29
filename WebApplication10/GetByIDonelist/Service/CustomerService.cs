namespace WebApplication10.GetByIDonelist.Service;

using Microsoft.Data.SqlClient;
using WebApplication10.DTOs;
using WebApplication10.Exceptions;

public class CustomerService(IConfiguration configuration) : ICustomerService
{
    public async Task<CustomerRentalsOnlyDto> GetCustomerRentalsOnlyAsync(int customerId)
    {
        CustomerRentalsOnlyDto? dto = null;

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
                                     s.name
                              FROM Customer c
                              LEFT JOIN Rental r ON c.customer_id = r.customer_id
                              LEFT JOIN Status s ON s.status_id = r.status_id
                              WHERE c.customer_id = @customerId
                              """;

        command.Parameters.AddWithValue("@customerId", customerId);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            dto ??= new CustomerRentalsOnlyDto
            {
                FirstName = reader.GetString(0),
                LastName = reader.GetString(1),
                Rentals = []
            };

            if (reader.IsDBNull(2))
            {
                continue;
            }

            var rentals = dto.Rentals.ToList();

            rentals.Add(new RentalOnlyDto
            {
                Id = reader.GetInt32(2),
                RentalDate = reader.GetDateTime(3),
                ReturnDate = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                Status = reader.GetString(5)
            });

            dto.Rentals = rentals;
        }

        if (dto is null)
        {
            throw new NotFoundExcpetion($"Customer with id {customerId} not found");
        }

        return dto;
    }
}