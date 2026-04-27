using Microsoft.Data.SqlClient;
using WebApplication10.DTOs;
using WebApplication10.Exceptions;
using WebApplication10.Services;

namespace WebApplication10.Services;

public class CustomerService(IConfiguration configuration) : ICustomerService
{
    public async Task<CustomerBasicDto> GetCustomerByIdAsync(int customerId)
    {
        await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        await connection.OpenAsync();

        command.Connection = connection;
        command.CommandText = """
                              SELECT customer_id,
                                     first_name,
                                     last_name
                              FROM Customer
                              WHERE customer_id = @customerId
                              """;

        command.Parameters.AddWithValue("@customerId", customerId);

        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            throw new NotFoundException($"Customer with id {customerId} not found");
        }

        return new CustomerBasicDto
        {
            Id = reader.GetInt32(0),
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2)
        };
    }
}