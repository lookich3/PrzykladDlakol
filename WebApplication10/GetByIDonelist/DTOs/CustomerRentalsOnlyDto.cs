namespace WebApplication10.GetByIDonelist.DTOs;

public class CustomerRentalsOnlyDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public IEnumerable<RentalOnlyDto> Rentals { get; set; } = [];
}