using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MathPro.Domain.Entities
{
    public class Message
    {
        // TODO: use or not to use complex key ?
        public string MessageId { get; set; }
                
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }

        public int RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public virtual ApplicationUser Recipient { get; set; }

        public DateTime Created { get; set; }
        // TODO: status is string ?
        public string Status { get; set; }
        
    
        public string Subject { get; set; }
        
        [Required]
        public string Body { get; set; }

    }
}
