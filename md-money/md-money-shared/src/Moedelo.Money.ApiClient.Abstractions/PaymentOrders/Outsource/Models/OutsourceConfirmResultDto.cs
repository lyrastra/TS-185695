using Moedelo.Money.Enums.Outsource;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models;

/// <summary>
/// Массовая работа с выписками: результат подтверждения ПП в ЛК
/// </summary>
public class OutsourceConfirmResultDto
{
    public long DocumentBaseId { get; set; }
    public OutsourceConfirmPaymentStatus Status { get; set; }
}