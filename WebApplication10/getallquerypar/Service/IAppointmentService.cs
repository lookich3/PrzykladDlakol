namespace WebApplication10.getallquerypar.Service;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(string? status, string? patientLastName);
}