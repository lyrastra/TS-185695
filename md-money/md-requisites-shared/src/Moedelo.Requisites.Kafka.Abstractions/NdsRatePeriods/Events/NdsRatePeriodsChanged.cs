using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.NdsRatePeriods.Events
{
    public class NdsRatePeriodsChanged : IEntityEventData
    {
        public int FirmId { get; set; }
       
        public IReadOnlyList<NdsRatePeriodEventData> Periods { get; set; }
    }
}