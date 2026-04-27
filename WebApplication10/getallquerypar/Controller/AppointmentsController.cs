using Microsoft.AspNetCore.Mvc;
using WebApplication10.getallquerypar.Service;

namespace WebApplication10.getallquerypar.Controller;

[ApiController]
[Route("api/appointments")]
public class AppointmentsController(IAppointmentService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAppointments(
        [FromQuery] string? status,
        [FromQuery] string? patientLastName)
    {
        return Ok(await service.GetAppointmentsAsync(status, patientLastName));
    }
}