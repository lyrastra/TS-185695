using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class UpdateIlliquidOpportunityCommand
    {
        public Guid SuiteCrmOpportunityId { get; set; }

        /// <summary>
        /// Статус лида из АО
        /// </summary>
        public string LeadStatus { get; set; }
    }
}