using System;
using Moedelo.BankIntegrations.Dto;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Acceptance.Sber
{
    public class AcceptanceAdvanceResponseDto : BaseResponseDto
    {
        public string ExternalId { get; set; }

        public string ContractNumber { get; set; }

        public DateTime AcceptStartDate { get; set; }

        public DateTime? AcceptLastDate { get; set; }

        public DateTime ContractDate { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public string Obligation { get; set; }

        public string PayeeAccount { get; set; }

        public string BankStatus { get; set; }

        public string BankComment { get; set; }

        public string PayeeName { get; set; }

        public string PayeeInn { get; set; }

        public string PayeeBankBic { get; set; }

        public string PayerAccount { get; set; }

        public string PayerName { get; set; }

        public string PayerInn { get; set; }

        public string PayerBic { get; set; }
    }
}
