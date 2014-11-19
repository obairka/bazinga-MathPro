using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPro.Domain.Entities
{
    public class MathAssignmentSubsectionViewModel
    {
        public MathAssignment mathAssignment { get; set; }
        public List<Subsection> subsections { get; set; }
    }
}
