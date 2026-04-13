using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedUser
{
    public class IntegrationPartnersInfoDto
    {
        /// <summary> Идентификатор партнёра </summary>
        public virtual IntegrationPartners IntegratedPartner { get; set; }

        /// <summary> Изображение Enable </summary>
        public virtual string ImgEnable { get; set; }

        /// <summary> Изображение Disable </summary>
        public virtual string ImgDisable { get; set; }

        /// <summary> Возможность отправлять платежки </summary>
        public virtual bool SendPaymentAvailable { get; set; }

        /// <summary> Возможность запрашивать выписки </summary>
        public virtual bool RequestExtractAvailable { get; set; }
    }
}
