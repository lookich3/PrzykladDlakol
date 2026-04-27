using WebApplication10.getByIdNstd.DTOs;

namespace WebApplication10.getByIdNstd.Service;

public interface ICustomerService
{
    Task<CustomerDto> GetCustomerRentalsAsync(int customerId);
}