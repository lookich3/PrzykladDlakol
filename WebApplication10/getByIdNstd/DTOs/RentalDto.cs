namespace WebApplication10.getByIdNstd.DTOs;

public class RentalDto
{
    public int Id { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public IEnumerable<MovieDto> Movies { get; set; } = [];
}