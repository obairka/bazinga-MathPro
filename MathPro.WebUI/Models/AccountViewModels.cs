﻿using MathPro.Domain.Entities;
using MathPro.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathPro.WebUI.Models
{
    public class UserProfileBriefViewModel
    {
        public UserProfileBriefViewModel() {}

        public UserProfileBriefViewModel(ApplicationUser user )
        {
            if (user != null)
            {
                UserName = user.UserName;
                Email = user.Email;
                Age = user.Age == null ? "-" : user.Age.ToString();
                Rating = user.Rating;
                UserId = user.Id;
                UnreadMessageCount = user.UnreadMessageCount;
                HasImage = user.HasImage;
                UserImageName = user.UserImageName;
            }            
        }
        public string UserId { get; set; }

        public int UnreadMessageCount { get; set; }

        public bool HasImage { get; set; }
        public string UserImageName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Логин должен быть не короче {2} символов", MinimumLength = 5)]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage="Введите свой email")]
        [EmailAddress]
        [Display(Name = "email")]
        public string Email { get; set; }


        [Display(Name = "Возраст")]
        public string Age
        {
            get;
            set;
        }

        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }
    }

    public class UserProfileViewModel : UserProfileBriefViewModel
    {
        public UserProfileViewModel() { }

        public UserProfileViewModel(ApplicationUser user)
            : base(user)
        {
            if (user != null)
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                BirthDate = user.BirthDate;
                LastVisitDate = user.LastVisitDate;
                RegistrationDate = user.RegistrationDate;
            }            
        }

        // [Required]
        [StringLength(30, ErrorMessage = "Имя должно быть не короче {2} символов.", MinimumLength = 2)]
        [Display(Name = "Имя")]
        public string FirstName 
        {
            get { return _firstname; }  
            set { _firstname = value ?? "-";  } 
        }

        // [Required]
        [StringLength(30, ErrorMessage = "Фамилия должна быть не короче {2} символов.", MinimumLength = 2)]
        [Display(Name = "Фамилия")]
        public string LastName 
        {
            get { return _lastname; } 
            set { _lastname = value ?? "-"; } 
        }

        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [PastDate(ErrorMessage = "Ты из будущего?")]
        [Display(Name = "Дата рождения")]
        public DateTime? BirthDate { get; set; }


        [Required]
        [Display(Name = "Последний визит")]
        public DateTime LastVisitDate { get; set; }
        [Required]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }


        public string FullName
        {
            get
            {
                // FullName - username if firstname or lastname wasn't set
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || FirstName.Equals("-") || LastName.Equals("-"))
                {
                    return UserName;
                }
                return FirstName + " " + LastName;
            }
        }


        protected string _firstname;
        protected string _lastname;
    }


    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : UserProfileBriefViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Пароль должен быть не короче {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
