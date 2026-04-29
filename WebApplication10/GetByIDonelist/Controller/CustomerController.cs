using Microsoft.AspNetCore.Mvc;
using WebApplication10.getall.Service;
using WebApplication10.GetByIDonelist.Service;

namespace WebApplication10.GetByIDonelist.Controller;


[ApiController]
[Route("api/customers")]
public class CustomersController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    [Route("{id:int}/rentals")]
    public async Task<IActionResult> GetCustomerRentalsOnly([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetCustomerRentalsOnlyAsync(id));
        }
        catch (NotFoundExcpetion e)
        {
            return NotFound(e.Message);
        }
    }
}