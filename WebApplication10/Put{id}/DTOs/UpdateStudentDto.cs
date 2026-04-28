namespace WebApplication10.Put_id_.DTOs;

public class UpdateStudentDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Pesel { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
}