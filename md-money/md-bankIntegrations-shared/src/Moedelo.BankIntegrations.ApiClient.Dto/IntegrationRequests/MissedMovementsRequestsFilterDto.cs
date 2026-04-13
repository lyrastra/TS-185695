using System.ComponentModel.DataAnnotations;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class MissedMovementsRequestsFilterDto
{
    public IntegrationPartners? Partner { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public int? FirmId { get; set; }

    public IntegrationCallType[] MovementListCallTypes { get; set; }
}
