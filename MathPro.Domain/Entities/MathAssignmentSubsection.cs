using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MathPro.Domain.Entities
{
    public class MathAssignmentSubsection
    {
        [Key]
        public int MathAssignmentSubsectionId { get; set; }

        [ForeignKey("MathAssignmentId")]
        public int MathAssignmentId { get; set; }

        [ForeignKey("SubsectionId")]
        public int SubsectionId { get; set; }
    }
}
