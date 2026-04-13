using Moedelo.Finances.Domain.Models.Reconciliation;
using Moedelo.Finances.Public.ClientData.Money.Reconciliation;

namespace Moedelo.Finances.Public.Mappers
{
    public static class ReconciliationReportMapper
    {
        public static ReconciliationReport Map (ReconciliationReportClientData clientData)
        {
            return new ReconciliationReport
            {
                SettlementAccountId = clientData.SettlementAccountId,
                BeginDate = clientData.BeginDate,
                EndDate = clientData.EndDate,
                Email = clientData.Email
            };
        }
    }
}