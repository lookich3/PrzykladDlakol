namespace WebApplication10.getallquerypar;

public class AppointmentDto
{
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public PatientBasicDto Patient { get; set; } = null!;
}