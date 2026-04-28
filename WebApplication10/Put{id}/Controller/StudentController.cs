using Microsoft.AspNetCore.Mvc;
using WebApplication10.Delete_id_.Exception;
using WebApplication10.Delete_id_.Service;
using WebApplication10.Put_id_.DTOs;
using WebApplication10.Put_id_.Service;

namespace WebApplication10.Put_id_.Controller;

[ApiController]
[Route("[controller]")]
public class StudentController(IStudentService service) : ControllerBase
{
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] int id,
        [FromBody] UpdateStudentDto dto)
    {
        try
        {
            await service.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}