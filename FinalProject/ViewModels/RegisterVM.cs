using System.ComponentModel.DataAnnotations;


namespace FinalProject.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [RegularExpression(@"^[a-zA-Z''-'ıəiöüçşğƏİÖÜÇŞ\s]{3,20}$",
         ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is a required field.")]
        [RegularExpression(@"^[a-zA-Z''-'ıəiöüçşğƏİÖÜÇŞ\s]{3,20}$",
            ErrorMessage = "Characters are not allowed.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Username is a required field.")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Must be between 4 and 15 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Enter valid Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field."), DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat Password is a required field."), DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The Password and Repeat Password fields did not match.")]
        public string RepeatPassword { get; set; }
    }
}
