using System;

namespace Moedelo.CommonV2.EventBus.Integrations
{
    public class SettlementBalanceEvent
    {
        public int FirmId { get; set; }

        /// <summary> Дата на которую закрываем операционный день </summary>
        public DateTime Date { get; set; }

        /// <summary> moedelo.dbo.IntegrationsRequests.Id </summary>
        public int IntegrationsRequestsId { get; set; }

        public string SettlementNumber { get; set; }

        /// <summary> Закрывающий баланс из банка </summary>
        public decimal ExcerptBalance { get; set; }
    }
}