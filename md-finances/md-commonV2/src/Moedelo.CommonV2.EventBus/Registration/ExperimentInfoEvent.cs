using System;

namespace Moedelo.CommonV2.EventBus.Registration
{
    public class ExperimentInfoEvent
    {
        public int UserId { get; set; }

        public bool IsInternal { get; set; }

        // Id эксперимента в Google Analytics
        public string ExperimentId { get; set; }

        // Id варианта в рамках этого эксперимента
        public string VariantId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}