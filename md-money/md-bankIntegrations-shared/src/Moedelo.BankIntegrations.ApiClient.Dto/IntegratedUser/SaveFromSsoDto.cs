using System;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class SaveFromSsoDto
    {
        public IntegrationPartners Partner { get; set; }
        public int FirmId { get; set; }
        public bool IsActive { get; set; }
        public string ExternalClientId { get; set; }

        /// <summary> Тип клиента Сбербанка Moedelo.Common.Enums.Enums.Integration.SberbankUserType </summary>
        public string UserType { get; set; }

        public string NameOrg { get; set; }

        /// <summary> Токен доступа к данным клиента </summary>
        public string AccessToken { get; set; }

        /// <summary> Токен для обновления токена доступа к данным клиента </summary>
        public string RefreshToken { get; set; }

        /// <summary> время последнего обновления токена </summary>
        public DateTime SessionLastDate { get; set; }

        /// <summary> Идентификатор нашего сервиса во внешней системе (OpenId Connect) </summary>
        public string ClientId { get; set; }
    }
}
