using System;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class BankStatementRequestByIntegrationPartner
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
    }
}