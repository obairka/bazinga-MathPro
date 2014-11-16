using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathPro.Domain.Entities
{
    public class UserAttempt
    {
        public int UserAttemptId { get; set; }
        
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int MathAssignmentId { get; set; }
        public MathAssignment MathAssignment { get; set; }

        [Required]
        public DateTime AttemptDateTime { get; set; }
        // User's answer: may be wrong or may be correct
        public string AssignmentAnswer { get; set; }

        // Value obtained after comparing AssignmentAnswer and MathAssignment.Answer
        public bool AttemptResultSuccess { get; set; }
    }
}
