namespace WebApplication10.getByIdNstd.DTOs;

public class CustomerDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public IEnumerable<RentalDto> Rentals { get; set; } = [];
}