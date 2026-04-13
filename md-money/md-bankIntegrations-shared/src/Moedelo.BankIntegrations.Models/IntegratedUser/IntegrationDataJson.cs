using System;
using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums.Integration;
using Newtonsoft.Json;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using SberUserType = Moedelo.BankIntegrations.Enums.Integration.SberbankUserType;

namespace Moedelo.BankIntegrations.Models.IntegratedUser
{
    public class IntegrationDataJson
    {
        /// <summary> Идентификатор в системе партнёра </summary>
        [JsonProperty(Order = 1)]
        public string CustomerId { get; set; }

        /// <summary> Бик или номер подразделения </summary>
        [JsonProperty(Order = 2)]
        public string BranchId { get; set; }

        /// <summary> р/сч </summary>
        [JsonProperty(Order = 3)]
        public string SettlementAccount { get; set; }

        /// <summary> Наименование организации </summary>
        [JsonProperty(Order = 4)]
        public string NameOrg { get; set; }

        /// <summary> Дата получения последней сессии для клиента </summary>
        [JsonProperty(Order = 7)]
        public DateTime? SessionLastDate { get; set; }

        /// <summary> Токен доступа к данным клиента </summary>
        [JsonProperty(Order = 9)]
        public string AccessToken { get; set; }

        /// <summary> Сбербанк. Тип клиента </summary>
        [JsonProperty(Order = 10)]
        private string UserType { get; set; }

        /// <summary> Сбербанк. Enum Тип клиента </summary>
        [JsonProperty(Order = 11)]
        public SberbankUserType? SberbankUserType { get; set; }

        /// <summary> Токен для обновления токена доступа к данным клиента </summary>
        [JsonProperty(Order = 12)]
        public string RefreshToken { get; set; }

        /// <summary> Идентификатор нашего сервиса во внешней системе (OpenId Connect) </summary>
        [JsonProperty(Order = 13)]
        public string ClientId { get; set; }

        /// <summary> Себрбанк. Прайс-лист для подписок </summary>
        [JsonProperty(Order = 14)]
        public int AcceptancePriceListId { get; set; }

        /// <summary> Дата изменений последнего шага по подпискам </summary>
        [JsonProperty(Order = 15)]
        public DateTime? AcceptanceLastDate { get; set; }

        /// <summary> Guid последнего выставленно ПТ в Сбербанк </summary>
        [JsonProperty(Order = 16)]
        public Guid? LastErrorPaymentGuid { get; set; }

        /// <summary> Количетсво неуспешных попыток выставления ПТ в Сбербанк </summary>
        [JsonProperty(Order = 17)]
        public int? ErrorPaymentCount { get; set; }

        /// <summary> Признак что счёт для подписок в сбербанка заблокирован </summary>
        [JsonProperty(Order = 18)]
        public bool? BlockedAcceptanceSettlement { get; set; }

        /// <summary> Дата последней проверки счёта в Сбере на блокировку </summary>
        [JsonProperty(Order = 19)]
        public DateTime? AcceptanceSettlementCheckLastDate { get; set; }

        [JsonProperty(Order = 20)]
        public bool? ClosedAcceptanceSettlement { get; set; }

        /// <summary> Наличие у пользователя патента </summary>
        [JsonProperty(Order = 21)]
        public bool? IsPatent { get; set; }
        
        /// <summary> Полученные доступные р/с из банка  </summary>
        [JsonProperty(Order = 22)]
        public List<BankAccount> BankAccounts { get; set; }
        
        public bool InitSberbankUserType()
        {
            if (string.IsNullOrWhiteSpace(UserType))
            {
                return false;
            }

            if (UserType.Equals("WEB", StringComparison.OrdinalIgnoreCase))
            {
                SberbankUserType = SberUserType.Sms;
                UserType = null;
                return true;
            }

            if (UserType.Equals("Token", StringComparison.OrdinalIgnoreCase))
            {
                SberbankUserType = SberUserType.Token;
                UserType = null;
                return true;
            }

            return false;
        }
    }
}