using WebApplication10.getall.Service;

namespace WebApplication10.getall.Controller;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/customers")]
public class CustomersController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        return Ok(await service.GetAllCustomersAsync());
    }
}