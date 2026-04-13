using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class AssignOpportunityToManagerCommand
    {
        public Guid SuiteCrmOpportunityId { get; set; }

        /// <summary>
        /// Id звонка из АО
        /// </summary>
        public int CallId { get; set; }

        /// <summary>
        /// Статус звонка из АО
        /// </summary>
        public string CallStatus { get; set; }

        /// <summary>
        /// Стратегия компании из АО
        /// </summary>
        public string CampaignStrategy { get; set; }

        public int? SuiteCrmManagerCallerId { get; set; }

        /// <summary>
        /// Длительность звонка из АО
        /// </summary>
        public int? TimeTalkCall { get; set; }

        /// <summary>
        /// Дата и время звонка из АО
        /// </summary>
        public DateTime? CallDate { get; set; }
    }
}