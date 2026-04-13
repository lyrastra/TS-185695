using System;

namespace Moedelo.Billing.Abstractions.Legacy.Dto;

public class PaymentHistoryDto
{
    public int Id { get; set; }

    public int PriceListId { get; set; }

    public int FirmId { get; set; }

    public string PaymentMethod { get; set; }

    public decimal Summ { get; set; }

    public long PaymentId { get; set; }

    public int RegionalPartnerId { get; set; }

    public int SalerId { get; set; }

    public bool Success { get; set; }

    public bool IsRefund { get; set; }

    public string Note { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int AgentId { get; set; }

    public int PromoCodeId { get; set; }

    public int ReferalId { get; set; }

    public DateTime? IncomingDate { get; set; }

    public DateTime? DocumentDate { get; set; }

    public bool? IsDownload { get; set; }

    public bool Reselling { get; set; }

    public int? /*TODO: Tariff?*/ OutsourcingTariff { get; set; }

    public decimal? DiscountSum { get; set; }

    public DateTime? RefundDate { get; set; }

    /// <summary>
    /// Транзакция передана в Google Tag Manager для маркетологов.
    /// </summary>
    public bool? Tracked { get; set; }
}