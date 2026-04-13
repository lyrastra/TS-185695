using System;

namespace Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

public class ConfirmClickReportPayment
{
    public long DocumentBaseId { get; set; }
    public DateTime CreateDate { get; set; }
    public int FirmId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public string KontragentName { get; set; }
    public string Description { get; set; }
    public int OperationType { get; set; }
    public decimal Sum { get; set; }
}