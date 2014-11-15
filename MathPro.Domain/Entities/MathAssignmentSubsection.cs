﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MathPro.Domain.Entities
{
    public class MathAssignmentSubsection
    {
        [Key]
        public int MathAssignmentSubsectionId { get; set; }

        [Required]
        public int MathAssignmentId { get; set; }

        [Required]
        public int SubsectionId { get; set; }


        public virtual ICollection<MathAssignment> MathAssignments { get; set; }

        public virtual ICollection<Subsection> Subsections { get; set; }
    }
}
