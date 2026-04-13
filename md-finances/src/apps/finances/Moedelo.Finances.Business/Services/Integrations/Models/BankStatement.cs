using System;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.Finances.Business.Services.Integrations.Models
{
    public class BankStatement
    {
        public BankStatement(int settlementAccountId, string settlementAccountNumber, int bankId)
        {
            SettlementAccountId = settlementAccountId;
            SettlementAccountNumber = settlementAccountNumber;
            BankId = bankId;
        }

        public int SettlementAccountId { get; private set; }

        public string SettlementAccountNumber { get; private set; }

        public int BankId { get; set; }

        public string BankBik { get; set; }

        public IntegrationPartners? IntegrationPartner { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
