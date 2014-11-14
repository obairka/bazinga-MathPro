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

        
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }

        
        public int ComplexityId { get; set; }
        public Complexity Complexity { get; set; }


        public string AssignmentText { get; set; }

        //if amount of points for assignment is different from default, then this value is used
        public int? PointsForAssignment { get; set; }

        public string Answer { get; set; }

        public virtual List<TaskComment> TaskComments { get; set; }

        public virtual ICollection<MathAssignmentSubsection> MathAssignmentSubsections { get; set; }
    }
}
