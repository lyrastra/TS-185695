using Moedelo.Billing.Shared.Enums;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.Marketplace.Events;

/// <summary>
/// Событие "Попытка продления"
/// </summary>
public class ProlongationAttemptEvent : IEntityEventData
{
    public int FirmId { get; set; }
        
    public int UserId { get; set; }

    /// <summary>
    /// Код продуктовой услуги
    /// </summary>
    public string ProductConfigurationCode { get; set; }

    /// <summary>
    /// Статус доступности продления
    /// </summary>
    public ProlongationAvailabilityStatus Status { get; set; }

    /// <summary>
    /// Данные продления аутсорсинга
    /// </summary>
    public AdditionalOutsourcingProlongationData OutsourcingProlongationData { get; set; }
}