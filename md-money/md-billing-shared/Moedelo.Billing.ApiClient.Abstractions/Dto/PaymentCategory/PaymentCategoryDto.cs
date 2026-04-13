using System;

namespace Moedelo.Billing.Abstractions.Dto.PaymentCategory;

public class PaymentCategoryDto
{
    public int PaymentHistoryId { get; set; }
    public int? FirmId { get; set; }
    public DateTime ModifyDate { get; set; }
    public CategoryDataDto CategoryData { get; set; }
}