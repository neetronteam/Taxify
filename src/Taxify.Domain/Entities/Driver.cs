using Taxify.Domain.Commons;

namespace Taxify.Domain.Entities;

public class Driver : Auditable
{
    public string CardNumber {get; set;}
    
    public long UserId { get; set;}
    public User User { get; set;}   

    public long AttachmentId { get; set;}
    public Attachment Attachment { get; set;}
}
