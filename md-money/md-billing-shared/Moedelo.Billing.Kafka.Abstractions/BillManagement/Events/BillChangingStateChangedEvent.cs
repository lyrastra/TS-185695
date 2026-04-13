using System;
using System.Collections.Generic;
using Moedelo.Billing.Shared.Enums.BillManagement;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.BillManagement.Events;

public class BillChangingStateChangedEvent: IEntityEventData
{
    /// <summary>
    /// Идентификатор запроса на изменение счёта
    /// </summary>
    public Guid RequestGuid { get; set; }

    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Временная метка изменения параметра счёта
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Полезная нагрузка. Сведения о процессе изменения
    /// </summary>
    public Dictionary<BillChangingParameterNamesEnum, object> Payload { get; set; }
    
    /// <summary>
    /// Статус запроса на изменение параметров счёта
    /// </summary>
    public BillChangingStatusEnum Status { get; set; }
}
