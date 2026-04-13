using Moedelo.Finances.Domain.Models.Integrations;
using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models
{
    public class SetupData
    {
        public DateTime? RegistrationDate { get; set; }

        public DateTime? BalanceDate { get; set; }

        public DateTime LastClosedDate { get; set; }

        public DateTime? RegistrationInService { get; set; }

        public AccessRuleFlags AccessRuleFlags { get; set; }

        public string ImportMessages { get; set; }

        public List<IntegrationErrorResponse> IntegrationErrors { get; set; }
    }
}
