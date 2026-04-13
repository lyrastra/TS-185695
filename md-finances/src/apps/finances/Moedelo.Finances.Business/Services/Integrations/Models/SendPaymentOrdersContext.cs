using System;
using System.Collections.Generic;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;

namespace Moedelo.Finances.Business.Services.Integrations.Models
{
    internal class SendPaymentOrdersContext
    {
        public Dictionary<int, BankDto> Banks { get; set; }
        public Dictionary<string, SettlementAccountStatusDto>  Integrations { get; set; }
        public Dictionary<int, List<OrderWithGuid>> OperationsBySettlementAccounts { get; set; }
        public Dictionary<Guid, OrderWithGuid> OperationsByGuid { get; set; }
        public Dictionary<int, SettlementAccountDto> SettlementAccounts { get; set; }
    }
}