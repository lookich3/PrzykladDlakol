using WebApplication10.GetByIDonelist.DTOs;

namespace WebApplication10.GetByIDonelist.Service;

public interface ICustomerService
{
    Task<CustomerRentalsOnlyDto> GetCustomerRentalsOnlyAsync(int customerId);
}