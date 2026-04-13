using Moedelo.Common.Enums.Enums.ElectronicReports;
using System;

namespace Moedelo.CommonV2.EventBus.Erpt
{
    public class NeformalDocumentsIonLoadAutomaticallyEvent
    {
        public int FirmId { get; set; }

        public string Login { get; set; }

        public int DocumentId { get; set; }

        public DateTime DocumentCreateDate { get; set; }

        public IonType Type { get; set; }

        public string Url { get; set; }
    }
}