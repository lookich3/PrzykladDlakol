using WebApplication10.DTOs;

namespace WebApplication10.Services;

public interface ICustomerService
{
    Task<CustomerBasicDto> GetCustomerByIdAsync(int customerId);
}