using System;

namespace Moedelo.Billing.Abstractions.Dto.YaPay;

public class YaPayOrderCreationResponseDto
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string MerchantId { get; set; }
    public decimal TotalSum { get; set; }
    public DataOrderDto Data { get; set; }
    public Guid OrderGuid { get; set; }

    public class DataOrderDto
    {
        public string PaymentUrl { get; set; }
    }
}