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
        [Key]
        public int ComplexityId { get; set; }
      
        public string Name { get; set; }

        public int DefaultPoints { get; set; }
    }
}
