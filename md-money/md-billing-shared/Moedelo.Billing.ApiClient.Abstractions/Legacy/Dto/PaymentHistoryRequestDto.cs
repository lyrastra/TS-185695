using System;

namespace Moedelo.Billing.Abstractions.Legacy.Dto;

/// <summary>
/// критерии поиска платежей
/// все указанные условия складываются через логическое И (умножение)
/// если вам необходимо ИЛИ - делайте несколько вызовов
/// </summary>
public class PaymentHistoryRequestDto
{
    /// <summary>
    /// Если указан, то только платежи с такими идентификаторами
    /// </summary>
    public int[] Ids { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи, со значением StartDate больше или равным указанному значению
    /// </summary>
    public DateTime? StartDateAfter { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи, со значением StartDate меньше или равным указанному значению
    /// </summary>
    public DateTime? StartDateBefore { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи, со значением ExpirationDate больше или равным указанному значению
    /// </summary>
    public DateTime? ExpirationDateAfter { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи, со значением ExpirationDate меньше или равным указанному значению
    /// </summary>
    public DateTime? ExpirationDateBefore { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи, со значением Date больше или равным указанному значению
    /// </summary>
    public DateTime? DateAfter { get; set; }

    /// <summary>
    /// Если указан, то только платежи, со значением Date меньше или равным указанному значению
    /// </summary>
    public DateTime? DateBefore { get; set; }

    /// <summary>
    /// Если указан, то только платежи, со значением IncomingDate больше или равным указанному значению
    /// </summary>
    public DateTime? IncomingDateAfter { get; set; }

    /// <summary>
    /// Если указан, то только платежи, со значением IncomingDate меньше или равным указанному значению
    /// </summary>
    public DateTime? IncomingDateBefore { get; set; }

    /// <summary>
    /// Если указан, то либо только успешны платежы (Success == true), либо только не успешные (Success == false)
    /// </summary>
    public bool? Success { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежы с указанным значением IsDownload
    /// </summary>
    public bool? IsDownload { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежы с указанным значением Reselling
    /// </summary>
    public bool? Reselling { get; set; } = null;

    /// <summary>
    /// Если указан, то либо только возвраты платежы (Success == 1), либо только не возвраты (Success == 0)
    /// </summary>
    public bool? IsRefund { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи с указанными Продавцами
    /// </summary>
    public int[] SellerIds { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи с указанными Региональными партнёрами
    /// </summary>
    public int[] RegionalPartnerIds { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи с указанными ReferralId
    /// </summary>
    public int[] ReferralIds { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи по указанным прайс-листам
    /// </summary>
    public int[] PriceListIds { get; set; } = null;

    /// <summary>
    /// Если указан, то только платежи по указанным фирмам
    /// </summary>
    public int[] FirmIds { get; set; } = null;

    /// <summary>
    /// Только платежи с указанными методами
    /// </summary>
    public string[] PaymentMethods { get; set; } = null;

    /// <summary>
    /// Исключить платежи с указанными методами
    /// </summary>
    public string[] ExcludePaymentMethods { get; set; } = null;

    /// <summary>
    /// Платежи с указанной суммой
    /// </summary>
    public decimal? Sum { get; set; }

    /// <summary>
    /// Если указан, то только платежи с указанным промо-кодом 
    /// </summary>
    public int? PromoCodeId { get; set; }
}