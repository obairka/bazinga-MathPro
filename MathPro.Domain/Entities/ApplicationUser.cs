using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MathPro.Domain.Infrastructure;

namespace MathPro.Domain.Entities
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        // BirthDate should be expressed as a date without time component
        
        [DataType(DataType.Date)]
        // BirthDate can't be in the future or present
        [PastDate(ErrorMessage = "Birthdate can't be in the future or present")]
        public DateTime? BirthDate { get; set; }

        
        [NotMapped]
        // TODO: delete : People cant live longer than 200 years :D
        [Range(0, 200)]
        public int Age 
        {
            get
            {
                return DateTime.Now.Year - BirthDate.Value.Year;
            }
            set
            {   // TODO: check
                // birthYear = (NowYear-Age)
                // dyear = oldBirthYear  - (NowYear-Age);
                // birthYear = oldBirthYear + dYear
                BirthDate.Value.AddYears(BirthDate.Value.Year - (DateTime.Now.Year - value) );
            }
        }
      
        [Required]
        public DateTime LastVisitDate { get; set; }
        
        [Required]        
        public DateTime RegistrationDate { get; set; }

        [Required]               
        public int Rating { get; set; }
    }
}