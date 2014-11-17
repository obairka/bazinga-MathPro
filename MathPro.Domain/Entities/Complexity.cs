using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Сложность")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Количество баллов")]
        public int DefaultPoints { get; set; }
    }
}
