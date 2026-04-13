using System;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

public class ReconciliationOperationDto
{
    public long Id { get; set; }
    public bool IsOutgoing { get; set; }
    public decimal Sum { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public int? KontragentId { get; set; }
    public string KontragentName { get; set; }
    public string DocumentSection { get; set; }
    public bool IsSalary { get; set; }
}