using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.AccountRequest
{
    public class AccountRequestDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public DateTime? RequestMovementStartDate { get; set; }
        public bool IsRequestMovements { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
    }
}
