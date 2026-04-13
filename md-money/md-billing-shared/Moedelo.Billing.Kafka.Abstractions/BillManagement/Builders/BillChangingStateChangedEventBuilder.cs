using System;
using System.Collections.Generic;
using Moedelo.Billing.Kafka.Abstractions.BillManagement.Events;
using Moedelo.Billing.Shared.Enums.BillManagement;

namespace Moedelo.Billing.Kafka.Abstractions.BillManagement.Builders;

public class BillChangingStateChangedEventBuilder
{
    private int firmId;
    private Guid requestGuid;
    private readonly Lazy<Dictionary<BillChangingParameterNamesEnum, object>> payload =
        new(() => []);
    private BillChangingStatusEnum status;

    public void SetFirmId(int firmId)
    {
        this.firmId = firmId;
    }

    public void SetStatus(BillChangingStatusEnum status)
    {
        this.status = status;
    }

    public void AddPayloadData(BillChangingParameterNamesEnum parameterName, object data)
    {
        payload.Value.Add(parameterName, data);
    }

    public void SetRequestGuid(Guid requestGuid)
    {
        this.requestGuid = requestGuid;
    }

    public BillChangingStateChangedEvent ToEvent()
    {
        return new BillChangingStateChangedEvent
        {
            FirmId = firmId,
            Payload = payload.Value,
            Timestamp = DateTime.UtcNow,
            Status = status,
            RequestGuid = requestGuid
        };
    }
}
