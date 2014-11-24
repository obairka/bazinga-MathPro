using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPro.Domain.Entities;

namespace MathPro.WebUI.Models
{
    public class MathAssignmentViewModel
    {
        public MathAssignment mathAssignment { get; set; }
        public IEnumerable<MathAssignment> mathAssignments { get; set; }
        public SubsectionViewModel subsections { get; set; }
        public DbSet<Complexity> complexities { get; set; }
        public DbSet<Section> sections { get; set; }
        public UserAttempt userAttempt { get; set; }
        public TaskComment taskComment { get; set; }
    }

    public class SubsectionViewModel
    {
        public List<Subsection> AvailableSubsection { get; set; }
        public List<Subsection> SelectedSubsection { get; set; }
        public PostedSubsections PostedSubsection { get; set; }
    }
    public class PostedSubsections
    {
        //this array will be used to POST values from the form to the controller
        public string[] SubsectionIds { get; set; }
    }
}
