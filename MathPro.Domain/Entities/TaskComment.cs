using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPro.Domain.Entities
{
    public class TaskComment
    {        
        public int TaskCommentId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int MathAssignmentId { get; set; }
        public MathAssignment MathAssignment { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public DateTime PostedTime { get; set; }
    }
}
