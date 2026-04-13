using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class MissedCallOnOpportunityCommand
    {
        public Guid SuiteCrmOpportunityId { get; set; }

        /// <summary>
        /// Статус лида из АО
        /// </summary>
        public string LeadStatus { get; set; }

        /// <summary>
        /// Id звонка из АО
        /// </summary>
        public int CallId { get; set; }

        /// <summary>
        /// Статус звонка из АО
        /// </summary>
        public string CallStatus { get; set; }
    }
}