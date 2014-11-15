using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPro.Domain.Entities
{
    public class Complexity
    {
        public int ComplexityId { get; set; }
      
        [Required]
        public string Name { get; set; }

        [Required]
        public int DefaultPoints { get; set; }
    }
}
