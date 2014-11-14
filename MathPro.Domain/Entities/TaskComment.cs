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

        
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        
        public int MathAssignmentId { get; set; }
        public MathAssignment MathAssignment { get; set; }

        public string Details { get; set; }

        public DateTime PostedTime { get; set; }
    }
}
