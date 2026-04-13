using System.ComponentModel.DataAnnotations;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class CountIntegrationRequestsByFilterDto
{
    public int FirmId { get; set; }

    public string SettlementNumber { get; set; }

    public IntegrationPartners IntegrationPartner { get; set; }

    public IntegrationCallType CallType { get; set; }

    public string DateOfCallAfter { get; set; }

    public string DateOfCallBefore { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }
}
