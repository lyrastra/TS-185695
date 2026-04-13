using Moedelo.Money.Enums.Outsource;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;

public class OutsourceConfirmResult
{
    public long DocumentBaseId { get; set; }
    public OutsourceConfirmPaymentStatus Status { get; set; }
}