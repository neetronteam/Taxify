using Taxify.Domain.Commons;

namespace Taxify.Domain.Entities;

public class CarModel : Auditable
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public short Version { get; set; }
}
