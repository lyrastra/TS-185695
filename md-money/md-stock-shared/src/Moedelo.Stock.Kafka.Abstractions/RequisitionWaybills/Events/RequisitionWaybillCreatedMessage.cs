using System;
using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Stock.Kafka.Abstractions.RequisitionWaybills.Events
{
    public class RequisitionWaybillCreatedMessage : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DocumentDate { get; set; }

        public IReadOnlyList<RequisitionWaybillItemSaveMessage> Items { get; set; }
    }
}