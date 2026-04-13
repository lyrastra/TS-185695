using System;
using Moedelo.Finances.Domain.Models.Reconciliation;
using Moedelo.Finances.Public.ClientData.Money.Reconciliation;

namespace Moedelo.Finances.Public.Mappers
{
    public static class ReconciliationReportForUserMapper
    {
        public static ReconciliationReportForUserModel Map (ReconciliationReportForUserClientData clientData)
        {
            return new ReconciliationReportForUserModel
            {
                SessionId = clientData.SessionId,
                ExcludeOperationsIds = clientData.ExcludeOperationsIds ?? Array.Empty<long>()
            };
        }
    }
}