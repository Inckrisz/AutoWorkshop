﻿namespace AutoWorkshopApi.Models;
using System.ComponentModel.DataAnnotations;

public class Client
{
    public int ClientId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    public ICollection<Job> Jobs { get; set; } = new List<Job>();
}


