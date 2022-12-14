using System.ComponentModel.DataAnnotations;

namespace BlogApp.UI.Models.Authentication;

public class LoginVM
{
    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Şifre")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
