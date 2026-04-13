using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class UpsaleClientInOutsourceCommand
    {
        public DateTime ExpirationDate { get; set; }
        public int FirmId { get; set; }
        /// <summary>
        /// Идентификатор контрагента в SuiteCrm
        /// </summary>
        public string AccountId { get; set; }
        public IEnumerable<TaskOptions> Tasks { get; set; }

        public class TaskOptions
        {
            public string Subject { get; set; }

            public string Description { get; set; }
        }
    }
}