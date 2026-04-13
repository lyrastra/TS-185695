using System;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;

public interface IConfirmPaymentOrderDto
{
    public long DocumentBaseId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; }
    public int SettlementAccountId { get; set; }
}