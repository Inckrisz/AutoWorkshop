namespace AutoWorkshopApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Client
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [NotEmptyOrWhitespace(ErrorMessage = "ClientId cannot be empty or contain only whitespace.")]
    public int ClientId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
    [NotEmptyOrWhitespace(ErrorMessage = "Name cannot be empty or contain only whitespace.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [NotEmptyOrWhitespace(ErrorMessage = "Address cannot be empty or contain only whitespace.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [NotEmptyOrWhitespace(ErrorMessage = "Email cannot be empty or contain only whitespace.")]
    public string Email { get; set; }

    public ICollection<Job> Jobs { get; set; } = new List<Job>();
}


