using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public  string Username { get; set; } = string.Empty; //ganto yung casing para sa JSON username siya, hindi camelcase, unlike nung nasa entities
    
    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;
}
