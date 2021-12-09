using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Password is a required field."), DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat Password is a required field."), DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The Password and Repeat Password fields did not match.")]
        public string RepeatPassword { get; set; }
    }
}
