using Taxify.Domain.Commons;
using Taxify.Domain.Enums;

namespace Taxify.Domain.Entities;

public class User : Auditable
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }
    public Role Role { get; set; }

    public long? AttachmentId {get;set; }
    public Attachment Attachment {get;set; }
}