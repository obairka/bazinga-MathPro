using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Номер задачи")]
        public int MathAssignmentId { get; set; }
        public MathAssignment MathAssignment { get; set; }

        [Required]
        [DisplayName("Комментарий")]
        public string Details { get; set; }

        [Required]
        [DisplayName("Время")]
        public DateTime PostedTime { get; set; }
    }
}
