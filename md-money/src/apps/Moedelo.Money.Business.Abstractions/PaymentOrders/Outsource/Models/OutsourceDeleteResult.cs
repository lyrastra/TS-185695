using Moedelo.Money.Enums.Outsource;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;

public class OutsourceDeleteResult
{
    public long DocumentBaseId { get; set; }
    public OutsourceDeletePaymentStatus Status { get; set; }
}