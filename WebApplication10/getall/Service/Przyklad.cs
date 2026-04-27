namespace WebApplication10.getall.Service;

public class Przyklad
{
using Microsoft.Data.SqlClient;
using WebApplication6.DTOs;
using WebApplication6.Exceptions;

namespace WebApplication6.Services;

public class CustomerService(IConfiguration configuration) : ICustomerService
{
    public async Task<CustomerDto> GetCustomerRentalsAsync(int customerId)
    {
        await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        await connection.OpenAsync();

        command.Connection = connection;

        command.CommandText = """

                              """;

        command.Parameters.AddWithValue("@customerId", customerId);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            
        }

        return null!;
    }
}
}