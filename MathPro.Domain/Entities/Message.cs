using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MathPro.Domain.Entities
{
    public class Message
    {
        // TODO: use or not to use complex key ?
        public int MessageId { get; set; }
        
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }

        
        public string RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public virtual ApplicationUser Recipient { get; set; }

        public DateTime Created { get; set; }
        
        // TODO: Default: IsRead = false
        [Required]
        public bool IsRead { get; set; }
        
        public string Subject { get; set; }
        
        [Required]
        public string Body { get; set; }

    }
}
