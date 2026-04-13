using System;

namespace Moedelo.Finances.Domain.Models.Reconciliation
{
    public class ReconciliationReport
    {
        /// <summary> Ид р/сч </summary>
        public int SettlementAccountId { get; set; }

        /// <summary> Период сверки </summary>
        public DateTime BeginDate { get; set; }

        /// <summary> Период сверки </summary>
        public DateTime EndDate { get; set; }

        /// <summary> Почта для отправки отчёта </summary>
        public string Email { get; set; }
    }
}
