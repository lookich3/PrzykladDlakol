using WebApplication10.Exceptions;
using WebApplication10.getByIdNstd.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication10.getByIdNstd.Controller;



[ApiController]
[Route("api/customers")]
public class CustomersController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    [Route("{id:int}/rentals")]
    public async Task<IActionResult> GetRentals([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetCustomerRentalsAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}