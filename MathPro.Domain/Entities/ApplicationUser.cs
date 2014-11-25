using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MathPro.Domain.Infrastructure;
using System.Collections.Generic;

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
    
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // BirthDate should be expressed as a date without time component
        
        [DataType(DataType.Date)]
        // BirthDate can't be in the future or present
        [PastDate]
        public DateTime? BirthDate { get; set; }

        
        [NotMapped]
        [Range(0, 200)]
        public int? Age 
        {
            get
            {
                if (null == BirthDate)
                {
                    return null;
                }
                DateTime today = DateTime.Today;
                int age = today.Year - BirthDate.Value.Year;
                if (BirthDate.Value > today.AddYears(-age)) age--;
                return age;
            }  
        }
      
        // TODO: auto setting
        [Required]
        public DateTime LastVisitDate { get; set; }
        
        [Required]        
        public DateTime RegistrationDate { get; set; }

        [Required]               
        public int Rating { get; set; }

        public virtual ICollection<TaskComment> TaskComments { get; set; }
        // All one user's attempts
        public virtual ICollection<UserAttempt> UserAttempts { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Message> MessagesISend { get; set; }
        [InverseProperty("Recipient")]
        public virtual ICollection<Message> MessagesIReceive { get; set; }

        [NotMapped]
        public IEnumerable<Message> MyMessages
        {
            get
            {
                if (MessagesISend == null)
                {
                    return new List<Message>();
                }

                return MessagesISend.Union(MessagesIReceive).OrderBy(m => m.CreatedOn);
            }
        }

        [NotMapped]
        // Users in converstion
        public IEnumerable<ApplicationUser> Interlocutors
        {
            get
            {
                if (MyMessages == null)
                {
                    return new List<ApplicationUser>();
                }
                return MyMessages.Select(m => m.SenderId != this.Id ? m.Sender : m.Recipient);
            }
        }

        public int UnreadMessageCount 
        {
            get
            {
                if (MessagesIReceive == null)
                {
                    return 0;
                }
                return MessagesIReceive.Where(m => !m.IsRead).Count();
            }
        }

    }
}