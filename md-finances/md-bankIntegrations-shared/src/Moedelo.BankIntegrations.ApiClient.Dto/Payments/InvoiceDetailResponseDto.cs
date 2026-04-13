using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Enums.Invoices;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class InvoiceDetailResponseDto : BaseResponseDto
    {
        public long DocumentBaseId { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public IntegrationPartners IntegrationPartnerId { get; set; }
        
        public string DescriptionStatus { get; set; }
        
        public InvoiceStatus Status { get; set; }
    }
}