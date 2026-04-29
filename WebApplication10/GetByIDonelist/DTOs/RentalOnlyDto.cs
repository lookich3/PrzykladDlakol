namespace WebApplication10.GetByIDonelist.DTOs;

public class RentalOnlyDto
{
    public int Id { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; } = string.Empty;
}