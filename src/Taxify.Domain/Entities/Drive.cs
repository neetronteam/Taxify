﻿using System.Security.Cryptography;
using Taxify.Domain.Commons;
using Taxify.Domain.Enums;

namespace Taxify.Domain.Entities;

public class Drive : Auditable
{
    public string Description { get; set; }
    public Decimal Price { get; set; }
    public PaymentType PaymentType { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Current { get; set; }
}