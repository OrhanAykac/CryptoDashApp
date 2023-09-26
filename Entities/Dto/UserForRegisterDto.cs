using System.ComponentModel.DataAnnotations;

namespace Entities.Dto;
public class UserForRegisterDto
{
    [Required, MinLength(3), MaxLength(32)]
    public string FirstName { get; set; }
    [Required, MinLength(3), MaxLength(32)]
    public string LastName { get; set; }

    [Required, MinLength(3), MaxLength(32)]
    [EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(3), MaxLength(32)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required, MinLength(3), MaxLength(32)]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
