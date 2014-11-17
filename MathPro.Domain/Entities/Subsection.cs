using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPro.Domain.Entities
{
    public class Subsection
    {
        
        public int SubsectionId { get; set; }

        [Required]
        [DisplayName("Подраздел")]
        public string Name { get; set; }

        public virtual ICollection<MathAssignment> MathAssignments{ get; set; }
    }
}
