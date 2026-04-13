using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Bankruptcy
{
    public class BaseMessageDto
    {
        public string CaseNumber { get; set; }

        public string CommissionerName { get; set; }

        public string CommissionerOrganisationName { get; set; }

        public string DebtorAddress { get; set; }

        public string DebtorName { get; set; }

        public string Note { get; set; }

        public string Number { get; set; }

        public DateTime? PublishDate { get; set; }
    }
}