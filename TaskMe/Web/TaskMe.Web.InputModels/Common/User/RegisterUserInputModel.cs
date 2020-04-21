﻿namespace TaskMe.Web.InputModels.Common.User
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class RegisterUserInputModel
    {
        [Required]
        [Display(Name = "First name")]
        [RegularExpression(@"[A-Z][a-z]+", ErrorMessage = "The name must contain only letters and starts with upper one.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [RegularExpression(@"[A-Z][a-z]+", ErrorMessage = "The name must contain only letters and starts with upper one.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IFormFile Picture { get; set; }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}