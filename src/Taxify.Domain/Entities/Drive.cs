using Taxify.Domain.Commons;
using Taxify.Domain.Enums;

namespace Taxify.Domain.Entities;

public class Drive : Auditable
{
    public string Description { get; set; }
    public decimal Price { get; set; }
    public PaymentType PaymentType { get; set; }
    public string Departure { get; set; }
    public string Destination { get; set; }
    public string Current { get; set; }
}
