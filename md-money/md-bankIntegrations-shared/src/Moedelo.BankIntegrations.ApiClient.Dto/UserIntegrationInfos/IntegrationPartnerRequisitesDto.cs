namespace Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos;

public class IntegrationPartnerRequisitesDto : IntegrationPartnerDto
{
    /// <summary> Текст ошибки в случае недоступности интеграции </summary>
    public string ErrorText { get; set; }
}