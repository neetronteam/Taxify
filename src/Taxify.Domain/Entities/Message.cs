using System.Reflection.Metadata.Ecma335;
using Taxify.Domain.Commons;

namespace Taxify.Domain.Entities;

public class Message : Auditable
{
    public string Content { get; set; }
    public long SenderId { get; set; } 
    public User Sender { get; set; }

    public long ReceiverId { get; set; }
    public User Receiver { get; set; }

    public long? AttachmentId { get; set; }
    public Attachment? Attachment { get; set; }
}
