namespace AutoWorkshop.Shared.DTOs;

using System.ComponentModel.DataAnnotations;

public class ClientDTO
{
    [NotEmptyOrWhitespace]
    public int ClientId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [NotEmptyOrWhitespace]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
    public string Name { get; set; }

    [NotEmptyOrWhitespace]

    [Required(ErrorMessage = "Address is required.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [NotEmptyOrWhitespace]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
}
