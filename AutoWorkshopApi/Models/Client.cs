namespace AutoWorkshopApi.Models;
using System.ComponentModel.DataAnnotations;

public class Client
{
    [Key]
    public int ClientId { get; set; }  // Automatikusan generált ID

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }  // Ügyfél neve

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }  // Lakcím

    [Required]
    [EmailAddress]
    public string Email { get; set; }  // Email cím (email validációval)
}


