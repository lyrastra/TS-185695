using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;

public class IntegrationErrorSaveRequestDto
{
    public int FirmId { get; set; }

    public IntegrationPartners Partner { get; set; }

    public IntegrationCallType CallType { get; set; }

    public bool IsManual { get; set; }

    public bool IsNeedDisableIntegration { get; set; }

    public string SettlementNumber { get; set; }

    public IntegrationErrorType ErrorType { get; set; }

    public string ErrorMessageForUser { get; set; }

    public int LastMacroRequestId { get; set; }
}
