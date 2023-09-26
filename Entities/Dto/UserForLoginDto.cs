using System.ComponentModel.DataAnnotations;

namespace RiseX.Entities.Dto;
public class UserForLoginDto
{

    [Required, MinLength(3), MaxLength(32)]
    [EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(3), MaxLength(32)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
