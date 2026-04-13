using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class WorkerDto
{
    public int Id { get; set; }
    public DateTime? DateOfStartWork { get; set; }
    public DateTime? TerminationDate { get; set; }
    public string CountryCode { get; set; }
}