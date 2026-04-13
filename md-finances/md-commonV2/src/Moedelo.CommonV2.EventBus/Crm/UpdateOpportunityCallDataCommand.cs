using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class UpdateOpportunityCallDataCommand
    {
        public Guid SuiteCrmOpportunityId { get; set; }

        /// <summary>
        /// Id звонка из АО
        /// </summary>
        public int CallId { get; set; }

        /// <summary>
        /// Кол-во звонков по лиду из АО
        /// </summary>
        public int? LeadAttemptCount { get; set; }

        /// <summary>
        /// Длительность звонка из АО
        /// </summary>
        public int? TimeTalkCall { get; set; }
    }
}