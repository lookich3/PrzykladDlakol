using WebApplication10.DTOs;

namespace WebApplication10.getall.Service;

using Microsoft.Data.SqlClient;


public class CustomerService(IConfiguration configuration) : ICustomerService
{
    public async Task<IEnumerable<CustomerBasicDto>> GetAllCustomersAsync()
    {
        List<CustomerBasicDto> customers = [];

        await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        await connection.OpenAsync();

        command.Connection = connection;
        command.CommandText = """
                              SELECT customer_id,
                                     first_name,
                                     last_name
                              FROM Customer
                              """;

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            customers.Add(new CustomerBasicDto
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2)
            });
        }

        return customers;
    }
}