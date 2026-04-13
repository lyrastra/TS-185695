using System;

namespace Moedelo.Billing.Abstractions.Legacy.Dto;

/// <summary>
/// состояние оплат фирмы по данным биллинга
/// ВНИМАНИЕ: не надо это использовать в прикладном коде, структура заведена только для использования в
/// новой системе авторизации. Не предназначено для использования в прикладном коде
/// </summary>
public class FirmBillingStateDto
{
    public enum FirmPaidStatus
    {
        /// <summary>
        /// у фирмы нет ни одного платежа
        /// </summary>
        NoOnePayment = 1,

        /// <summary>
        /// находится на действующем триальном платеже
        /// </summary>
        Trial = 2,

        /// <summary>
        /// находится на действующем нетриальном платеже
        /// </summary>
        Paid = 3,

        /// <summary>
        /// нет действующего платежа, но есть платёж, действие которого окончено
        /// </summary>
        Expired = 4,
    }

    public int FirmId { get; set; }
    public FirmPaidStatus PaidStatus { get; set; }
    public DateTime StartDate { get; set; }

    /// <summary>
    /// это поле оставлено только обратной совместимости. Скоро понятие тариф исчезнет и по нему нельзя будет ничего определить
    /// </summary>
    public int[] TariffIds { get; set; }
    
    public string TariffName { get; set; }

    public string ProductPlatform { get; set; }
}