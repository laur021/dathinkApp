using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public required string Username { get; set; } //ganto yung casing para sa JSON username siya, hindi camelcase, unlike nung nasa entities
    [Required]
    public required string Password { get; set; }
}
