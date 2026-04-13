#nullable enable
using System;
using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.PaymentHistory;

public class PaymentHistoryPaidFirmIdsRequestDto
{
    public DateTime SamplingDate { get; set; }
    
    public IReadOnlyCollection<string>? PaymentMethods { get; set; }
}