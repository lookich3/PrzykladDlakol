using WebApplication10.DTOs;

namespace WebApplication10.getall.Service;

public interface ICustomerService
{
    Task<IEnumerable<CustomerBasicDto>> GetAllCustomersAsync();
}