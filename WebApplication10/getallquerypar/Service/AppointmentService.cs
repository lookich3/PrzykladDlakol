using Microsoft.Data.SqlClient;

namespace WebApplication10.getallquerypar.Service;

public class AppointmentService(IConfiguration configuration) : IAppointmentService
{
    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(string? status, string? patientLastName)
    {
        List<AppointmentDto> appointments = [];

        await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        await connection.OpenAsync();

        command.Connection = connection;
        command.CommandText = """
                              SELECT a.appointment_id,
                                     a.appointment_date,
                                     a.status,
                                     p.first_name,
                                     p.last_name
                              FROM Appointment a
                              JOIN Patient p ON p.patient_id = a.patient_id
                              WHERE (@status IS NULL OR a.status = @status)
                                AND (@patientLastName IS NULL OR p.last_name = @patientLastName)
                              """;

        command.Parameters.AddWithValue("@status", status is null ? DBNull.Value : status);
        command.Parameters.AddWithValue("@patientLastName", patientLastName is null ? DBNull.Value : patientLastName);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            appointments.Add(new AppointmentDto
            {
                Id = reader.GetInt32(0),
                AppointmentDate = reader.GetDateTime(1),
                Status = reader.GetString(2),
                Patient = new PatientBasicDto
                {
                    FirstName = reader.GetString(3),
                    LastName = reader.GetString(4)
                }
            });
        }

        return appointments;
    }
}