using Taxify.Domain.Commons;

namespace Taxify.Domain.Entities;

public class Car : Auditable
{
    public string Number { get; set; }
    
    public long CarModelId { get; set; }
    public CarModel CarModel { get; set; }

    public long ColorId { get; set; }
    public Color Color { get; set; }

    public long AttachmentId { get; set; }
    public Attachment Attachment { get; set; }
}
