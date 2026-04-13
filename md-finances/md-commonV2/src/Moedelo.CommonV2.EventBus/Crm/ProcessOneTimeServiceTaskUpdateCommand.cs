using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class ProcessOneTimeServiceTaskUpdateCommand
    {
        public int FirmId { get; set; }

        public int OutsourcingClientId { get; set; }

        public string Login { get; set; }

        public int OutsourcingTaskId { get; set; }

        public string TaskTitle { get; set; }

        public string TaskDescription { get; set; }

        public DateTime? CompletionDate { get; set; }
    }
}