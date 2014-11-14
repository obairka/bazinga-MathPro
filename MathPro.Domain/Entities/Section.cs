using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPro.Domain.Entities
{
    public class Section
    {
        
        public int SectionId { get; set; }

        public string Name { get; set; }
    }
}
