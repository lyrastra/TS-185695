using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;

public class IntegrationPartnersInfoDto
{
    /// <summary> Идентификатор партнёра </summary>
    public IntegrationPartners IntegratedPartner { get; set; }

    /// <summary> Изображение Enable </summary>
    public string ImgEnable { get; set; }

    /// <summary> Изображение Disable </summary>
    public string ImgDisable { get; set; }

    /// <summary> Возможность отправлять платежки </summary>
    public bool SendPaymentAvailable { get; set; }

    /// <summary> Возможность запрашивать выписки </summary>
    public bool RequestExtractAvailable { get; set; }
}