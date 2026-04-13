using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Acceptance.Sber
{
    public class AcceptanceAdvanceRequestDto
    {
        public int FirmId { get; set; }
        public int IntegrationPartner { get; set; }
        public string ContractNumber { get; set; }
        public string AcceptStartDate { get; set; }
        public string AcceptLastDate { get; set; }
        public string ContractDate { get; set; }
        public string Number { get; set; }
        public Guid ExternalId { get; set; }
        public string Date { get; set; }
        public string Obligation { get; set; }
        public string PayeeAccount { get; set; }
        public string PayeeName { get; set; }
        public string PayeeInn { get; set; }
        public string PayeeBankBic { get; set; }
        public string PayerAccount { get; set; }
        public string PayerName { get; set; }
        public string PayerInn { get; set; }
        public string PayerBic { get; set; }
        public string BackUrl { get; set; }
    }
}