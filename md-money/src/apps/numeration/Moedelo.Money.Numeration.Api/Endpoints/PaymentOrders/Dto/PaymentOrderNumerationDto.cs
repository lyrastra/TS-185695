using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Numeration.Api.Endpoints.PaymentOrders.Dto;

public class PaymentOrderNumerationDto
{
    [Required]
    public int SettlementAccountId { get; set; }

    [Required]
    public int Year { get; set; }

    [Required]
    public int LastNumber { get; set; }
}