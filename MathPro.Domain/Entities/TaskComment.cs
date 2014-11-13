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
        [Key]
        public int TaskCommentId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public int ApplicationUserId { get; set; }

        [ForeignKey("MathAssignmentId")]
        public int MathAssignmentId { get; set; }

        public string Details { get; set; }

        public DateTime PostedTime { get; set; }
    }
}
