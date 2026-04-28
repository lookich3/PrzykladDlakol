using Microsoft.AspNetCore.Mvc;
using WebApplication10.Delete_id_.Exception;
using WebApplication10.Delete_id_.Service;

namespace WebApplication10.Delete_id_.Controller;
[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStundentService service) : ControllerBase
{
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await service.RemoveAsync(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}