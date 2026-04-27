using Microsoft.AspNetCore.Mvc;
using WebApplication10.Exceptions;
using WebApplication10.Services;

namespace WebApplication10.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetCustomerByIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}