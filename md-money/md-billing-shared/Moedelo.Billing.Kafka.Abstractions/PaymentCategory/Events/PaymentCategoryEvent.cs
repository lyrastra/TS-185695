using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.PaymentCategory.Events;

public class PaymentCategoryEvent : IEntityEventData
{
    public int PaymentHistoryId { get; set; }
    public int FirmId { get; set; }
    public DateTime ModifyDate { get; set; }
    public bool IsManualEditing { get; set; }
    public string CategoryData { get; set; }
}