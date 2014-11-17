using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPro.Domain.Entities
{
    public class MathAssignment
    {
        
        public int MathAssignmentId { get; set; }

        [Required]
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }

        [Required]
        public int ComplexityId { get; set; }
        public Complexity Complexity { get; set; }

        [Required]
        public string AssignmentText { get; set; }

        //if amount of points for assignment is different from default, then this value is used
        public int? PointsForAssignment { get; set; }

        [Required]
        public string Answer { get; set; }

        public virtual List<TaskComment> TaskComments { get; set; }

        public virtual ICollection<Subsection> Subsections { get; set; }

        // All users' all attempts on this assignment
        public virtual ICollection<UserAttempt> UserAttempts { get; set; }
    }
}
