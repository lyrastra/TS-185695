using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

public class OutsourceStateUpdateResult
{
    public long DocumentBaseId { get; set; }
    public int FirmId { get; set; }
    public OutsourceState? OutsourceState { get; set; }
}