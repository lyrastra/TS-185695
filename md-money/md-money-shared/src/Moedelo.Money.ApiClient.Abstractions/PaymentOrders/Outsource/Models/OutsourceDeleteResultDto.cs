using Moedelo.Money.Enums.Outsource;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models;

/// <summary>
/// Массовая работа с выписками: результат удаления ПП в ЛК
/// </summary>
public class OutsourceDeleteResultDto
{
    public long DocumentBaseId { get; set; }
    public OutsourceDeletePaymentStatus Status { get; set; }
}