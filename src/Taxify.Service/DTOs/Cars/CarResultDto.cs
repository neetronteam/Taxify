using Taxify.Service.DTOs.Attachments;
using Taxify.Service.DTOs.CarModels;
using Taxify.Service.DTOs.Colors;

namespace Taxify.Service.DTOs.Cars;

public class CarResultDto
{
    public string Number { get; set; }
    public CarModelResultDto CarModel { get; set; }
    public ColorResultDto Color { get; set; }
    public AttachmentResultDto Attachment { get; set; }
}