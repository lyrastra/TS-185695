using System;
using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Legacy.Dto;

public class FirmPackagesInfoDto
{
    public int FirmId { get; set; }
    public IEnumerable<PackageInfoDto> Packages { get; set; }
}

public class PackageInfoDto
{
    public int PaymentId { get; set; }
    public string Name { get; set; }
    public string ProductCode { get; set; }
    public decimal Price { get; set; }
    public DateTime IncomingDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}